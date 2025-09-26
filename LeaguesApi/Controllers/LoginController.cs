

using LeaguesApi.Attributes;
using LeaguesApi.Dtos;
using LeaguesApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;

namespace LeaguesApi.Controllers;

[ApiController]
[Route("/api/admin/sessions")]
public class LoginController : ControllerBase
{
    private readonly IAdminService _adminService;

    public LoginController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var (admin,token) = await _adminService.Login(loginRequest.Email, loginRequest.Password);
        if (token is not null && admin is not null)
        {
            return Ok(new LoginResponse()
            {
                Id = admin.Id,
                Email = admin.Email,
                Token = token
            });
        }

        return Unauthorized(new {message = "Wrong Email or Password"});
    }
    


}