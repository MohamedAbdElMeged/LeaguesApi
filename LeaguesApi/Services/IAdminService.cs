using LeaguesApi.Dtos;
using LeaguesApi.Models;

namespace LeaguesApi.Services;

public interface IAdminService
{
    public Task<Admin> GetAdminByEmailAsync(string email);
    public Task<(Admin,string)> Login(string email,string password);
    public Admin GetAdminById(int id);
    public Task<(Admin?, string?)> CreateNewAdminAsync(CreateNewAdminRequest newAdminRequest);
}

