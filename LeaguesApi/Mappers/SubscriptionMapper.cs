using LeaguesApi.Dtos.Responses;
using LeaguesApi.Dtos.Requests;
using LeaguesApi.Models;

namespace LeaguesApi.Mappers;

public static class SubscriptionMapper
{
    public static SubscriptionResponse ToSubscriptionResponse(this Subscription subscription)
    {
        return new SubscriptionResponse()
        {
            Id = subscription.Id,
            IsActive = subscription.IsActive,
            RemainingQuota = subscription.RemainingQuota,
            SeasonId = subscription.SeasonId,
            SubscriptionDate = subscription.SubscriptionDate,
            SubscriptionPlan = subscription.SubscriptionPlan.ToSubscriptionPlanResponse()
        };
    }

    public static Subscription ToSubscriptionFromCreateSubscriptionRequest
        (this CreateNewSubscriptionRequest createNewSubscriptionRequest)
    {
        return new Subscription()
        {
            SubscriberId = createNewSubscriptionRequest.SubscriberId,
            SubscriptionPlanId = createNewSubscriptionRequest.SubscriptionPlanId,
            SeasonId = createNewSubscriptionRequest.SeasonId
        };
    }
}