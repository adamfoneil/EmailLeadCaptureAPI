using Dapper.Entities.Abstractions.Interfaces;
using Dapper.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailLeadCapture.Database;

[Table("email_lead")]
public class EmailLead : IEntity<int>, IAlternateKey
{
	public int Id { get; set; }
	[ForeignKey(nameof(Application))]
	public int ApplicationId { get; set; }
	[MaxLength(50)]
	public string Email { get; set; } = default!;
	public bool IsConfirmed { get; set; }
	public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;	
	public DateTime? ConfirmedUtc { get; set; }
	public bool IsOptedIn { get; set; } = true;
	public DateTime? OptChangedUtc { get; set; }

	public IEnumerable<string> AlternateKeyColumns => [nameof(ApplicationId), nameof(Email)];
}
