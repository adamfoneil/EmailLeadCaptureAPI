using EmailLeadCapture.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmailLeadCapture.API.EFData;

public class LeadCaptureDbContext(string connectionString) : DbContext
{
	private readonly string ConnectionString = connectionString;

	public DbSet<EmailLead> EmailLeads { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(ConnectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EmailLead>().HasAlternateKey(nameof(EmailLead.Application), nameof(EmailLead.Email));		
	}
}

public class LeadCaptureDbContextFactory : IDesignTimeDbContextFactory<LeadCaptureDbContext>
{
	public LeadCaptureDbContext CreateDbContext(string[] args)
	{
		var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
		return new(config.GetConnectionString("Default") ?? throw new Exception("connection string not found"));
	}		
}
