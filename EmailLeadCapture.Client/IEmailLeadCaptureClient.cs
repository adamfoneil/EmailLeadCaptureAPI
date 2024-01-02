using EmailLeadCapture.Shared;
using Refit;

namespace EmailLeadCapture.Client;

public interface IEmailLeadCaptureClient
{
	[Post("/confirm/{leadId}")]
	Task ConfirmAsync(string leadId);
	[Post("/optout/{leadId")]
	Task OptOutAsync(string leadId);
	[Post("/optin/{leadId}")]
	Task OptInAsync(string leadId);
	[Get("/api/applications")]
	Task<IEnumerable<Application>> GetApplicationsAsync();
	[Post("/api/{applicationId}/save")]
	Task<EmailLead> SaveLeadAsync(string applicationId, string email);
}