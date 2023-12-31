using Microsoft.AspNetCore.Authentication;

namespace EmailLeadCapture.API;

public class AllowedKeyOptions : AuthenticationSchemeOptions
{
    public string[] Values { get; set; } = [];
}
