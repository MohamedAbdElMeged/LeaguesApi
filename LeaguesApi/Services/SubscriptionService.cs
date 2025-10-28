using AutoMapper;
using LeaguesApi.Data;
using LeaguesApi.Dtos;
using LeaguesApi.Models;
using Microsoft.EntityFrameworkCore;
using LeaguesApi.Dtos.Requests;
namespace LeaguesApi.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ApplicationDbContext _context;

    public SubscriptionService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<SubscriptionPlan>> GetSubscriptionPlans()
    {
        return await _context.SubscriptionPlans.ToListAsync();
    }

    public async Task<SubscriptionResponse> CreateSubscription(CreateNewSubscriptionRequest createNewSubscriptionRequest)
    {
        var subscriptionPlan = await GetSubscriptionPlanById(createNewSubscriptionRequest.SubscriptionPlanId);
        var existingSubscription = await _context.Subscriptions.FirstOrDefaultAsync(s =>
            s.IsActive == false && s.SubscriptionPlanId == subscriptionPlan.Id &&
                s.SeasonId == createNewSubscriptionRequest.SeasonId &&
                s.SubscriberId == createNewSubscriptionRequest.SubscriberId);
        if (existingSubscription != null)
        {
            await ToggleSubscription(existingSubscription);
            return new SubscriptionResponse(); // need change
        }
        var subscription = new Subscription()
        {
            SubscriberId = createNewSubscriptionRequest.SubscriberId,
            SubscriptionPlanId = createNewSubscriptionRequest.SubscriptionPlanId,
            SeasonId = createNewSubscriptionRequest.SeasonId,
            RemainingQuota = subscriptionPlan.Quota
        };
        
        await _context.Subscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();
        return new SubscriptionResponse(); // need change
    }

    public async Task<SubscriptionPlan> GetSubscriptionPlanById(int id)
    {
        return await _context.SubscriptionPlans.FirstOrDefaultAsync(sp => sp.Id == id);
    }

    public async Task<Subscription> GetSubscriptionById(int id, int subscriberId)
    {
        return await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == id &&
                                                                     s.SubscriberId == subscriberId) ;
    }

    public async Task ToggleSubscription(Subscription subscription)
    {
        subscription.IsActive = !subscription.IsActive;
        _context.Subscriptions.Update(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task<List<SubscriptionResponse>> GetSubscriptionsBySubscriberId(int subscriberId)
    {
        var subscriptions =  await _context.Subscriptions.Where(s => s.SubscriberId == subscriberId).ToListAsync();
        return new List<SubscriptionResponse>(); // need change
    }
}