using EmailLeadCapture.API;
using EmailLeadCapture.API.EFData;
using EmailLeadCapture.Database;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Secret.json", optional: true, reloadOnChange: true);
builder.Services.AddScoped((sp) => new Hashids(builder.Configuration["HashIds:Salt"], int.Parse(builder.Configuration["HashIds:MinLength"]!)));

var allowedKeys = builder.Configuration.GetSection("AllowedKeys").Get<string[]>();

builder.Services.AddAuthentication("AllowedKeys")
	.AddScheme<AllowedKeyOptions, AllowedKeyAuthHandler>("AllowedKeys", options =>
	{
		options.Values = allowedKeys ?? throw new Exception("missing AllowedKeys configuration");
	});

builder.Services.AddAuthorization();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContext<LeadCaptureDbContext>();
builder.Services.AddSingleton<LeadCaptureDatabase>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "AO Lead Capture API");

app.MapPost("/confirm/{leadId}", async (string leadId, LeadCaptureDatabase database, Hashids hashIds) =>
{
	var emailLeadId = hashIds.DecodeSingle(leadId);
	await database.DoTransactionAsync(async (cn, txn) =>
	{
		var emailLead = await database.EmailLeads.GetAsync(cn, emailLeadId, txn) ?? throw new Exception("Lead not found");
		emailLead.IsConfirmed = true;
		emailLead.ConfirmedDateUtc = DateTime.UtcNow;
		await database.EmailLeads.SaveAsync(cn, emailLead, txn);
	});	
});

app.MapPost("/optout/{leadId}", async (string leadId, LeadCaptureDatabase database, Hashids hashIds) => 
	await SetOptStatus(leadId, database, hashIds, OptStatus.Out));

app.MapPost("/optin/{leadId}", async (string leadId, LeadCaptureDatabase database, Hashids hashIds) => 
	await SetOptStatus(leadId, database, hashIds, OptStatus.In));

async Task SetOptStatus(string leadId, LeadCaptureDatabase database, Hashids hashIds, OptStatus optStatus)
{
	var emailLeadId = hashIds.DecodeSingle(leadId);
	await database.DoTransactionAsync(async (cn, txn) =>
	{
		var emailLead = await database.EmailLeads.GetAsync(cn, emailLeadId, txn) ?? throw new Exception("Lead not found");
		emailLead.OptStatus = optStatus;
		emailLead.OptStatusChangedUtc = DateTime.UtcNow;
		await database.EmailLeads.SaveAsync(emailLead);
	});
}

var apiRoutes = app.MapGroup("/api").RequireAuthorization();

apiRoutes.MapGet("/encode", (int number, Hashids hashIds) =>
{
	return new { value = hashIds.Encode(number) };
});

apiRoutes.MapGet("/applications", async (LeadCaptureDbContext db, Hashids hashIds) =>
{
	return await db.Applications.Select(row => new { row.Name, id = hashIds.Encode(row.Id) }).ToListAsync();
});

apiRoutes.MapPost("/{appId}/save", async (string appId, Hashids hashIds, LeadCaptureDatabase database, string email) =>
{
	var applicationId = hashIds.DecodeSingle(appId);

	await database.EmailLeads.MergeAsync(new()
	{
		ApplicationId = applicationId,
		Email = email
	});

	var app = await database.Applications.GetAsync(applicationId);

	// todo: send email with link to confirmation page
});

apiRoutes.MapGet("/{appId}/leads", async (string appId, LeadCaptureDbContext db, Hashids hashIds, int page = 0) =>
{
	const int pageSize = 50;
	var applicationId = hashIds.DecodeSingle(appId);
	return await db.EmailLeads
		.Where(row => row.ApplicationId == applicationId && row.IsConfirmed && row.OptStatus == OptStatus.In)
		.OrderBy(row => row.Email)
		.Skip(page * pageSize).Take(pageSize)
		.ToListAsync();
});

app.Run();
