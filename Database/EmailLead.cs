﻿using Dapper.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailLeadCapture.Database;

public enum OptStatus
{
	// okay to email
	In = 1,
	// don't email
	Out = 0
}

[Table("email_lead")]
public class EmailLead : IEntity<int>
{
	public int Id { get; set; }
	public string Application { get; set; } = default!;
	public string Email { get; set; } = default!;
	public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;	
	public OptStatus OptStatus { get; set; } = OptStatus.In;
	public DateTime? OptStatusChangedUtc { get; set; }
}
