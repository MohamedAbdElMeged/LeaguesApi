using LeaguesApi.Models;

namespace LeaguesApi.Services;

public interface IAdminService
{
    public Admin GetAdminByEmail(string email);
    public Task<(Admin,string)> Login(string email,string password);
    public Admin GetAdminById(int id);
}

