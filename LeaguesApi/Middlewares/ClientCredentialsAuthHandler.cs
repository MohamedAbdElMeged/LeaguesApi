using System.Security.Claims;
using System.Text.Encodings.Web;
using LeaguesApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace LeaguesApi.Middlewares;

public class ClientCredentialsAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ISubscriberService _subscriberService;

    public ClientCredentialsAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ISubscriberService subscriberService
    ) : base(options, logger, encoder, clock)
    {
        _subscriberService = subscriberService;
    }

    protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("X-Client-Id", out var clientId) ||
            !Request.Headers.TryGetValue("X-Client-Secret", out var clientSecret))
        {
            return await Task.FromResult(AuthenticateResult.Fail("Missing credentials"));
        }

        var subscriber = await _subscriberService.GetSubscriberAsync(clientId, clientSecret);
        if (subscriber == null)
        {
            return await Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, clientId),
            new Claim(ClaimTypes.Name, subscriber.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }
}