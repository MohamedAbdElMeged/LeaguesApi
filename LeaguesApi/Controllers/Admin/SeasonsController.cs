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
[Route("api/admin/seasons")]
public class SeasonsController : ControllerBase
{
    private readonly ISeasonService _seasonService;
    private readonly IValidator<CreateSeasonRequest> _validator;

    public SeasonsController(ISeasonService seasonService, IValidator<CreateSeasonRequest> validator)
    {
        _seasonService = seasonService;
        _validator = validator;
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "JwtPolicy")]
    [SwaggerJwtAuth]
    public async Task<IActionResult> GetSeason(int id)
    {
        var season = _seasonService.GetSeasonById(id);
        return Ok(season);
    }

    [HttpPost()]
    [Authorize(Policy = "JwtPolicy")]
    [SwaggerJwtAuth]
    public async Task<IActionResult> CreateSeason([FromBody] CreateSeasonRequest createSeasonRequest)
    {
        var validationResult = await _validator.ValidateAsync(createSeasonRequest);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException(string.Join(" , ", validationResult.Errors.Select(x => x.ErrorMessage)));
            
        }

        var season = await _seasonService.CreateSeason(createSeasonRequest);
        return Ok(season.ToSeasonResposeFromEntity());
    }
    
}