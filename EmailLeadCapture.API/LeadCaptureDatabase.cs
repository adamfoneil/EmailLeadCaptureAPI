using Dapper.Entities;
using Dapper.Entities.Interfaces;
using Dapper.Entities.PostgreSql;
using EmailLeadCapture.Database;

namespace EmailLeadCapture.API;

public class LeadCaptureDatabase(string connectionString, ILogger<LeadCaptureDatabase> logger) : PostgreSqlDatabase(connectionString, logger)
{
	public EmailLeads EmailLeads => new(this);
}

public class BaseRepository<TEntity>(LeadCaptureDatabase database) : Repository<LeadCaptureDatabase, TEntity, int>(database) where TEntity : IEntity<int>
{
}

public class EmailLeads(LeadCaptureDatabase database) : BaseRepository<EmailLead>(database)
{
}