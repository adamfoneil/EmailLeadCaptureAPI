using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ProductIdeas;

public class AllowedKeyAuthHandler(
	IOptionsMonitor<AllowedKeyOptions> options,
	ILoggerFactory logger,
	UrlEncoder encoder) : AuthenticationHandler<AllowedKeyOptions>(options, logger, encoder)
{
	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		var allowedKeys = Options.Values.ToHashSet();

		if (Request.Headers.TryGetValue("api-key", out var headerKeyValue))
		{
			if (allowedKeys.Contains(headerKeyValue!)) return await Task.FromResult(AuthenticateResult.Success(ApiUser()));
		}

		if (Request.Query.TryGetValue("api-key", out var queryKeyValue))
		{
			if (allowedKeys.Contains(queryKeyValue!)) return await Task.FromResult(AuthenticateResult.Success(ApiUser()));
		}

		return await Task.FromResult(AuthenticateResult.Fail("Invalid login attempt"));

		AuthenticationTicket ApiUser()
		{
			var claims = new[] { new Claim(ClaimTypes.Name, "ApiUser") };
			var identity = new ClaimsIdentity(claims, Scheme.Name);
			var principal = new ClaimsPrincipal(identity);
			return new(principal, Scheme.Name);
		}
	}
}

