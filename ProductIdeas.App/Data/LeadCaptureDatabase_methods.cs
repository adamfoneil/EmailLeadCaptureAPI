using ProductIdeas.Database;

namespace ProductIdeas.Data;

public partial class LeadCaptureDatabase
{
	public async Task<string> SaveLeadAsync(int applicationId, string email) =>
		await DoTransactionAsync(async (cn, txn) =>
		{
			var existing = await EmailLeads.GetAlternateAsync(cn, new EmailLead() { ApplicationId = applicationId, Email = email }, txn);
			if (existing != null)
			{
				return HashIds.Encode(existing.Id);                
			}

			var result = await EmailLeads.SaveAsync(cn, new()
			{
				ApplicationId = applicationId,
				Email = email
			}, transaction: txn);

			var app = await Applications.GetAsync(cn, applicationId, txn);

			// todo: send email with link to confirmation page

			return HashIds.Encode(result.Id);
		});
}
