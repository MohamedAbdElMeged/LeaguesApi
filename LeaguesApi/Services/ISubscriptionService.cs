using LeaguesApi.Dtos;
using LeaguesApi.Models;

namespace LeaguesApi.Services;

public interface ISubscriptionService
{
    public Task<List<SubscriptionPlan>> GetSubscriptionPlans();

    public Task<SubscriptionResponse> CreateSubscription(CreateNewSubscriptionRequest createNewSubscriptionRequest);

    public Task<SubscriptionPlan> GetSubscriptionPlanById(int id);
    public Task<Subscription> GetSubscriptionById(int id, int subscriberId);
    public Task ToggleSubscription(Subscription subscription);
}