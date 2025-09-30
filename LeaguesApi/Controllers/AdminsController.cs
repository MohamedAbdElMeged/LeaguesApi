using FluentValidation;
using LeaguesApi.Attributes;
using LeaguesApi.Dtos;
using LeaguesApi.Services;
using LeaguesApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LeaguesApi.Controllers;

[ApiController]
[Route("/api/admin/admins")]
public class AdminsController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IValidator<CreateNewAdminRequest> _validator;

    public AdminsController(IAdminService adminService, IValidator<CreateNewAdminRequest> validator)
    {
        _adminService = adminService;
        _validator = validator;
    }
    
    [HttpPost("CreateNewAdmin")]
    [Authorize(Policy = "JwtPolicy")]
    [SwaggerJwtAuth]
    public async Task<IActionResult> CreateNewAdmin([FromBody] CreateNewAdminRequest createNewAdminRequest)
    {

        var validationResult = await _validator.ValidateAsync(createNewAdminRequest);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        return Ok(await _adminService.CreateNewAdminAsync(createNewAdminRequest));
    }
}