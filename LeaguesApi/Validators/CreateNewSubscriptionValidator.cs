using System.Data;
using FluentValidation;
using LeaguesApi.Data;
using LeaguesApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi.Validators;

public class CreateNewSubscriptionValidator : AbstractValidator<CreateNewSubscriptionRequest>
{
    private readonly ApplicationDbContext _context;
    public CreateNewSubscriptionValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.SubscriptionPlanId)
            .MustAsync(async (subscriptionPlanId, _) =>
                await _context.SubscriptionPlans.AnyAsync(s => s.Id == subscriptionPlanId))
            .WithMessage("Subscription Plan doesn't exist");
        RuleFor(x => x.SeasonId)
            .MustAsync(async (seasonId, _) =>
                await _context.Seasons.AnyAsync(s => s.Id == seasonId))
            .WithMessage("Season doesn't exist");
        RuleFor(x => x)
            .MustAsync(async (x, _) =>
                !await _context.Subscriptions.AnyAsync(s =>
                    s.SeasonId == x.SeasonId && s.SubscriptionPlanId == x.SubscriptionPlanId 
                                             && s.SubscriberId == x.SubscriberId
                                             && s.IsActive == true)
            ).WithMessage("You already Subscribed");

    }
}