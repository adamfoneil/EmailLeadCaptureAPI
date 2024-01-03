using Microsoft.AspNetCore.Authentication;

namespace ProductIdeas;

public class AllowedKeyOptions : AuthenticationSchemeOptions
{
    public string[] Values { get; set; } = [];
}
