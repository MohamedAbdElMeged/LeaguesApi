using LeaguesApi.Models;

namespace LeaguesApi.Services;

public interface ISubscriberService
{
    public Task<Subscriber> GetSubscriberAsync(string clientId, string clientSecret);
    public Task UpdateQuota(Subscriber subscriber);
}