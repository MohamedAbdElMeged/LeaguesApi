using LeaguesApi.Dtos;
using LeaguesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaguesApi.Controllers;

public class SubscribersController : ControllerBase
{
    private readonly ISubscriberService _subscriberService;

    public SubscribersController(ISubscriberService subscriberService)
    {
        _subscriberService = subscriberService;
    }

    public Task<IActionResult> JoinLeagueApi([FromBody] CreateNewSubscriberRequest createNewSubscriberRequest)
    {
        _subscriberService.CreateNewSubscriberAsync(createNewSubscriberRequest);
    }
}