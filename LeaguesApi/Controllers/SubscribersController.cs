using FluentValidation;
using LeaguesApi.Dtos;
using LeaguesApi.Dtos.Requests;
using LeaguesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaguesApi.Controllers;
[ApiController]
[Route("/api/subscribers")]
public class SubscribersController : ControllerBase
{
    private readonly ISubscriberService _subscriberService;
    private readonly IValidator<CreateNewSubscriberRequest> _validator;

    public SubscribersController(ISubscriberService subscriberService, IValidator<CreateNewSubscriberRequest> validator)
    {
        _subscriberService = subscriberService;
        _validator = validator;
    }

    [HttpPost("JoinLeagueApi")]
    public async Task<IActionResult> JoinLeagueApi([FromBody] CreateNewSubscriberRequest createNewSubscriberRequest)
    {
        var validationResult = await _validator.ValidateAsync(createNewSubscriberRequest);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        var subscriber = await _subscriberService.CreateNewSubscriberAsync(createNewSubscriberRequest);
        return Ok(subscriber);
    }
}