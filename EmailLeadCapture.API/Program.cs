using Dapper;
using EmailLeadCapture.API;
using EmailLeadCapture.API.Queries;
using EmailLeadCapture.Database;
using HashidsNet;

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
builder.Services.AddSingleton<LeadCaptureDatabase>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "AO Lead Capture API");

var apiRoutes = app.MapGroup("/api").RequireAuthorization();

apiRoutes.MapGet("/encode", (int number, Hashids hashIds) =>
{
	return new { value = hashIds.Encode(number) };
});

apiRoutes.MapGet("/applications", async (LeadCaptureDatabase database, Hashids hashIds) =>
{
	var results = await new ListApplications().ExecuteAsync(database.GetConnection);
	return results.Select(row => new { row.Name, id = hashIds.Encode(row.Id) });
});

apiRoutes.MapPost("/{appId}/save", async (string appId, Hashids hashIds, LeadCaptureDatabase database, string email) =>
{
	var applicationId = hashIds.DecodeSingle(appId);
	await database.EmailLeads.SaveAsync(new()
	{
		ApplicationId = applicationId,
		Email = email
	});
	// todo: send email with link to confirmation page
});

app.Run();
