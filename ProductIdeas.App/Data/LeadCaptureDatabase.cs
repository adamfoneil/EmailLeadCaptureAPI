using Dapper.Entities;
using Dapper.Entities.Interfaces;
using Dapper.Entities.PostgreSql;
using HashidsNet;
using Mailgun;
using Microsoft.Extensions.Options;
using ProductIdeas.Database;

namespace ProductIdeas.Data;

public class ConnectionStrings
{
    public string Default { get; set; } = default!;
}

public partial class LeadCaptureDatabase(IOptions<ConnectionStrings> options, ILogger<LeadCaptureDatabase> logger, Hashids hashIds, MailgunClient mailgunClient) :
    PostgreSqlDatabase(options.Value.Default, logger, new DefaultSqlBuilder()
    {
        CaseConversion = CaseConversionOptions.Exact
    })
{
    private readonly Hashids HashIds = hashIds;
    private readonly MailgunClient MailgunClient = mailgunClient;

    public BaseRepository<EmailLead> EmailLeads => new(this);
    public BaseRepository<Application> Applications => new(this);    
}

public class BaseRepository<TEntity>(LeadCaptureDatabase database) : Repository<LeadCaptureDatabase, TEntity, int>(database) where TEntity : IEntity<int>
{
}
