using LeaguesApi.Data;
using LeaguesApi.Dtos;
using LeaguesApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
    public async Task<Admin> GetAdminByEmailAsync(string email)
    {
        return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
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

    public async Task<(Admin?, string?)> CreateNewAdminAsync(CreateNewAdminRequest newAdminRequest)
    {
        var existedAdmin = await GetAdminByEmailAsync(newAdminRequest.Email);
        if (existedAdmin != null)
        {
            return (null, "Email is already exists");
        }

        var newAdmin = new Admin()
        {
            Email = newAdminRequest.Email
        };
        newAdmin.Password =_passwordHasher.HashPassword(newAdmin, "123456");
        _context.Admins.Add(newAdmin);
        _context.SaveChanges();
        return (newAdmin, "Admin Added Successfully");
    }
}