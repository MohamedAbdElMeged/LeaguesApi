using LeaguesApi.Data;
using LeaguesApi.Models;
using Microsoft.AspNetCore.Identity;

namespace LeaguesApi.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<Admin> _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public AdminService(ApplicationDbContext context,
        IPasswordHasher<Admin> passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }
    public Admin GetAdminByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<(Admin, string)> Login(string email, string password)
    {
        var admin = _context.Admins.FirstOrDefault(a => a.Email.ToLower() == email.ToLower());
        if (admin is not null && 
            _passwordHasher.VerifyHashedPassword(admin, admin.Password, password) == PasswordVerificationResult.Success)
        {
            return (admin, _jwtTokenService.GenerateToken(admin.Email));
        }

        return (null, null);
    }

    public Admin GetAdminById(int id)
    {
        return _context.Admins.FirstOrDefault(a => a.Id == id);
    }
}