using Microsoft.AspNetCore.Authentication;

namespace EmailLeadCapture.API.Models;

public class AllowedKeyOptions : AuthenticationSchemeOptions
{	
	public string[] Values { get; set; } = [];
}
