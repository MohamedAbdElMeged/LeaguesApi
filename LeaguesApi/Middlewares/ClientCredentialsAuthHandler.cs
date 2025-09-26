using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace LeaguesApi.Middlewares;

public class ClientCredentialsAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public ClientCredentialsAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
    ) : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("X-Client-Id", out var clientId) ||
            !Request.Headers.TryGetValue("X-Client-Secret", out var clientSecret))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing credentials"));
        }
        
        if (clientId != "my-client" || clientSecret != "my-secret")
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, clientId),
            new Claim(ClaimTypes.Name, "API Client")
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}