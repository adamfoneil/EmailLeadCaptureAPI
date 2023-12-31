using EmailLeadCapture.API.Models;

namespace EmailLeadCapture.API.Interfaces;

public interface IEmaiLeadlStore
{
	Task<bool> ExistsAsync(string application, string email);
	Task<int> InsertAsync(EmailLead emailLead);
	Task SetOptStatus(int id, OptStatus optStatus);
	Task<IEnumerable<EmailLead>> QueryAsync(string application);
}
