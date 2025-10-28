using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using LeaguesApi.Attributes;
using LeaguesApi.Dtos;
using LeaguesApi.Dtos.Requests;
using LeaguesApi.Models;
using LeaguesApi.Services;
using LeaguesApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaguesApi.Controllers;
[ApiController]
[Route("/api/subscribers/subscriptions")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    
    private readonly IValidator<CreateNewSubscriptionRequest> _validator;
    

    public SubscriptionsController(ISubscriptionService subscriptionService,
        IValidator<CreateNewSubscriptionRequest> validator
        )
    {
        _subscriptionService = subscriptionService;
        _validator = validator;
        
    }

    [HttpGet("GetSubscriptionPlans")]
    [Authorize(Policy = "ClientPolicy")]
    [SwaggerClientAuth]
    public async Task<IActionResult> GetSubscriptionPlans()
    {
        var subscriptionPlans = await _subscriptionService.GetSubscriptionPlans();
        return Ok(subscriptionPlans);
    }

    [HttpPost("CreateSubscription")]
    [Authorize(Policy = "ClientPolicy")]
    [SwaggerClientAuth]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateNewSubscriptionRequest createNewSubscriptionRequest)
    {
        
        var subscriberId = Int32.Parse(User.Identity?.Name);
        createNewSubscriptionRequest.SubscriberId = subscriberId;
        var validationResult = await _validator.ValidateAsync(createNewSubscriptionRequest);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        return Ok(await _subscriptionService.CreateSubscription(createNewSubscriptionRequest));
    }
    
    [HttpPatch("/{id}/DeactivateSubscription")]
    [Authorize(Policy = "ClientPolicy")]
    [SwaggerClientAuth]
    public async Task<IActionResult> DeactivateSubscription([FromRoute] int id)
    {
        var subscriberId = Int32.Parse(User.Identity?.Name);
        var subscription = await _subscriptionService.GetSubscriptionById(id, subscriberId);
        if (subscription == null)
        {
            return BadRequest("Subscription not found");
        }

        if (!subscription.IsActive)
        {
            return BadRequest("Subscription already inactive");
        }

        await _subscriptionService.ToggleSubscription(subscription);
        return Ok(subscription);

    }

    [HttpGet("/GetSubscriptions")]
    [Authorize(Policy = "ClientPolicy")]
    [SwaggerClientAuth]
    public async Task<IActionResult> GetSubscriptions()
    {
        var subscriberId = Int32.Parse(User.Identity?.Name);
        return Ok(await _subscriptionService.GetSubscriptionsBySubscriberId(subscriberId));
    }
}