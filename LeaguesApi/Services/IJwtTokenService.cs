namespace LeaguesApi.Services;

public interface IJwtTokenService
{
    string GenerateToken(string email);
    public string? ValidateToken(string token);
}