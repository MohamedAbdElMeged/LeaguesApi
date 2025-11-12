using FluentValidation;
using LeaguesApi.Attributes;
using LeaguesApi.Dtos.Requests;
using LeaguesApi.Exceptions;
using LeaguesApi.Mappers;
using LeaguesApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaguesApi.Controllers.Admin;

[ApiController]
[Route("/api/admin/leagues")]
public class LeaguesController : ControllerBase
{
    private readonly ILeagueService _leagueService;
    private readonly IValidator<CreateLeagueRequest> _validator;

    public LeaguesController(ILeagueService leagueService, IValidator<CreateLeagueRequest> validator)
    {
        _leagueService = leagueService;
        _validator = validator;
    }

    [HttpPost("CreateNewLeague")]
    [Authorize(Policy = "JwtPolicy")]
    [SwaggerJwtAuth]
    public async Task<IActionResult> CreateNewLeague([FromBody] CreateLeagueRequest createLeagueRequest)
    {
        var validationResult = await _validator.ValidateAsync(createLeagueRequest);
        if (!validationResult.IsValid)
        {
            
            throw new BadRequestException(string.Join(" , ", validationResult.Errors.Select(x => x.ErrorMessage)));
        }

        var league = await _leagueService.CreateLeague(createLeagueRequest);
        return Ok(league.ToLeagueResponse()); 
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "JwtPolicy")]
    [SwaggerJwtAuth]
    public async Task<IActionResult> GetLeague(int id)
    {
        var league = await _leagueService.GetLeagueById(id);
        return Ok(league.ToLeagueResponse());
    }

    [HttpGet()]
    [Authorize(Policy = "JwtPolicy")]
    [SwaggerJwtAuth]
    public async Task<IActionResult> GetAll([FromQuery] LeaguesFilterRequest leaguesFilterRequest)
    {
        var leagues = await _leagueService.GetAllLeagues(leaguesFilterRequest);
        return Ok(leagues.Select(l => l.ToLeagueResponse()));
    }
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "JwtPolicy")]
    [SwaggerJwtAuth]
    public async Task<IActionResult> DeleteLeague(int id)
    {
        var league = await _leagueService.DeleteLeague(id);
        return Ok(league);
    }
}