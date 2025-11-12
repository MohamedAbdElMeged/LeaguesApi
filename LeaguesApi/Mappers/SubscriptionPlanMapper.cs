using LeaguesApi.Dtos.Responses;
using LeaguesApi.Models;

namespace LeaguesApi.Mappers;

public static class SubscriptionPlanMapper
{
    public static SubscriptionPlanResponse ToSubscriptionPlanResponse
        (this SubscriptionPlan subscriptionPlan)
    {
        return new SubscriptionPlanResponse()
        {
            Id = subscriptionPlan.Id,
            Name = subscriptionPlan.Name,
            Quota = subscriptionPlan.Quota
        };
    }
}