using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using ProductIdeas.App.Data;
using ProductIdeas.Database;

namespace ProductIdeas.Data;

public class LeadCaptureDbContext(IOptions<ConnectionStrings> options) : DbContext()
{
	private readonly string ConnectionString = options.Value.Default;

	public DbSet<EmailLead> EmailLeads { get; set; }
	public DbSet<Application> Applications { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(ConnectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EmailLead>().HasAlternateKey(nameof(EmailLead.ApplicationId), nameof(EmailLead.Email));

		modelBuilder.Entity<Application>().HasAlternateKey(nameof(Application.Name));		
	}
}

public class LeadCaptureDbContextFactory : IDesignTimeDbContextFactory<LeadCaptureDbContext>
{
	public LeadCaptureDbContext CreateDbContext(string[] args)
	{
		var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
		var connectionStrings = new ConnectionStrings();
		config.GetSection("ConnectionStrings").Bind(connectionStrings);
		var options = Options.Create(connectionStrings);
		return new(options);
	}		
}
