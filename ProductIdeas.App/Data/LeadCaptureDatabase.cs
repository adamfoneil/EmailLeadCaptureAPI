using Dapper.Entities;
using Dapper.Entities.Interfaces;
using Dapper.Entities.PostgreSql;
using Microsoft.Extensions.Options;
using ProductIdeas.Database;

namespace ProductIdeas.Data;

public class ConnectionStrings
{
    public string Default { get; set; } = default!;
}

public class LeadCaptureDatabase(IOptions<ConnectionStrings> options, ILogger<LeadCaptureDatabase> logger) :
    PostgreSqlDatabase(options.Value.Default, logger, new DefaultSqlBuilder()
    {
        CaseConversion = CaseConversionOptions.Exact
    })
{
    public BaseRepository<EmailLead> EmailLeads => new(this);
    public BaseRepository<Application> Applications => new(this);
}

public class BaseRepository<TEntity>(LeadCaptureDatabase database) : Repository<LeadCaptureDatabase, TEntity, int>(database) where TEntity : IEntity<int>
{
}
