using ProductIdeas.Shared;
using Refit;

namespace ProductIdeas.Client;

public interface IEmailLeadCaptureClient
{
	[Post("/confirm/{leadId}")]
	Task ConfirmAsync(string leadId);
	[Post("/optout/{leadId")]
	Task OptOutAsync(string leadId);
	[Post("/optin/{leadId}")]
	Task OptInAsync(string leadId);
	[Get("/api/applications")]
	Task<IEnumerable<Application>> GetApplicationsAsync([Header("api-key")]string apiKey);
	[Post("/api/save")]
	Task<EmailLead> SaveLeadAsync([Header("api-key")]string apiKey, [Body]EmailLead emailLead);
}