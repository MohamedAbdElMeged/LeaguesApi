using System.Security.Claims;
using System.Text.Encodings.Web;
using LeaguesApi.Exceptions;
using LeaguesApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace LeaguesApi.Middlewares;

public class JwtAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IAdminService _adminService;
    private readonly IJwtTokenService _jwtTokenService;


    public JwtAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IAdminService adminService,
        IJwtTokenService jwtTokenService
    ) : base(options, logger, encoder, clock)
    {
        _adminService = adminService;
        _jwtTokenService = jwtTokenService;
    }

    protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authorizationHeader) )
        {
            throw new UnauthorizedException("Missing Creds");
        }

        var adminId = _jwtTokenService.ValidateToken(authorizationHeader);
        if (adminId == null)
        {
            throw new UnauthorizedException("Invalid Creds");
        }

        var admin =  _adminService.GetAdminById(int.Parse(adminId));
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, authorizationHeader),
            new Claim(ClaimTypes.Name, admin.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }
}