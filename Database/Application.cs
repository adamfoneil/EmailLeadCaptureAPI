using Dapper.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailLeadCapture.Database;

[Table("application")]
public class Application : IEntity<int>
{
	public int Id { get; set; }
	[MaxLength(50)]
	public string Name { get; set; } = default!;
	[MaxLength(65)]
	public string MailgunApiKey { get; set; } = default!;

	public virtual ICollection<EmailLead> EmailLeads { get; set; } = [];
}
