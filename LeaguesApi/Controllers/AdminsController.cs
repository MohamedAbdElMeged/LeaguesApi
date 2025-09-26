using LeaguesApi.Attributes;
using LeaguesApi.Dtos;
using LeaguesApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LeaguesApi.Controllers;

[ApiController]
[Route("/api/admin/admins")]
public class AdminsController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminsController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    [HttpPost("CreateNewAdmin")]
    [Authorize(Policy = "JwtPolicy")]
    [SwaggerJwtAuth]
    public async Task<IActionResult> CreateNewAdmin([FromBody] CreateNewAdminRequest createNewAdminRequest)
    {

        var result = await _adminService.CreateNewAdminAsync(createNewAdminRequest);
        if (result.Item1 is not null)
        {
            return Created(result.Item2, result.Item1 );
        }

        return BadRequest(result.Item2);
    }
}