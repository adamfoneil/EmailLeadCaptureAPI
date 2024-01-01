using Dapper.Entities;
using Dapper.Entities.Interfaces;
using Dapper.Entities.PostgreSql;
using EmailLeadCapture.Database;
using Microsoft.Extensions.Options;

namespace EmailLeadCapture.API;

public class ConnectionStrings
{
	public string Default { get; set; } = default!;
}

public class LeadCaptureDatabase(IOptions<ConnectionStrings> options, ILogger<LeadCaptureDatabase> logger) : PostgreSqlDatabase(options.Value.Default, logger)
{
	public EmailLeads EmailLeads => new(this);
}

public class BaseRepository<TEntity>(LeadCaptureDatabase database) : Repository<LeadCaptureDatabase, TEntity, int>(database) where TEntity : IEntity<int>
{
}

public class EmailLeads(LeadCaptureDatabase database) : BaseRepository<EmailLead>(database)
{
}