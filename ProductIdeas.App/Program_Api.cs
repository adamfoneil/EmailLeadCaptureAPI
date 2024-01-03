using HashidsNet;
using Microsoft.EntityFrameworkCore;
using ProductIdeas.Data;
using ProductIdeas.Shared;

internal partial class Program
{	
	static void MapApiEndpoints(WebApplication app)
	{
		app.MapPost("/confirm/{leadId}", async (string leadId, LeadCaptureDatabase database, Hashids hashIds) =>
		{
			var emailLeadId = hashIds.DecodeSingle(leadId);
			await database.DoTransactionAsync(async (cn, txn) =>
			{
				var emailLead = await database.EmailLeads.GetAsync(cn, emailLeadId, txn) ?? throw new Exception("Lead not found");
				emailLead.IsConfirmed = true;
				emailLead.ConfirmedUtc = DateTime.UtcNow;
				await database.EmailLeads.SaveAsync(cn, emailLead, txn);
			});
		});

		app.MapPost("/optout/{leadId}", async (string leadId, LeadCaptureDatabase database, Hashids hashIds) =>
			await SetOptStatus(leadId, database, hashIds, false));

		app.MapPost("/optin/{leadId}", async (string leadId, LeadCaptureDatabase database, Hashids hashIds) =>
			await SetOptStatus(leadId, database, hashIds, true));

		async Task SetOptStatus(string leadId, LeadCaptureDatabase database, Hashids hashIds, bool optIn)
		{
			var emailLeadId = hashIds.DecodeSingle(leadId);
			await database.DoTransactionAsync(async (cn, txn) =>
			{
				var emailLead = await database.EmailLeads.GetAsync(cn, emailLeadId, txn) ?? throw new Exception("Lead not found");
				emailLead.IsOptedIn = optIn;
				emailLead.OptChangedUtc = DateTime.UtcNow;
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

		apiRoutes.MapPost("/save", async (Hashids hashIds, LeadCaptureDatabase database, EmailLead emailLead) =>
		{
			var applicationId = hashIds.DecodeSingle(emailLead.ApplicationId);

			var result = await database.EmailLeads.MergeAsync(new()
			{
				ApplicationId = applicationId,
				Email = emailLead.Email
			});

			var app = await database.Applications.GetAsync(applicationId);

			// todo: send email with link to confirmation page

			return new { id = hashIds.Encode(result.Id) };
		});

		apiRoutes.MapGet("/{appId}/leads", async (string appId, LeadCaptureDbContext db, Hashids hashIds, int page = 0) =>
		{
			const int pageSize = 50;
			var applicationId = hashIds.DecodeSingle(appId);
			return await db.EmailLeads
				.Where(row => row.ApplicationId == applicationId && row.IsConfirmed && row.IsOptedIn)
				.OrderBy(row => row.Email)
				.Skip(page * pageSize).Take(pageSize)
				.Select(row => new EmailLead() { Id = hashIds.Encode(row.Id), Email = row.Email })
				.ToListAsync();
		});

	}
}
