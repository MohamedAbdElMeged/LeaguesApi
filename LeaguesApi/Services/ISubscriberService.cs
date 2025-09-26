using LeaguesApi.Dtos;
using LeaguesApi.Models;

namespace LeaguesApi.Services;

public interface ISubscriberService
{
    public Task<Subscriber> GetSubscriberAsync(string clientId, string clientSecret);
    public Task<Subscriber> GetSubscriberByName(string name);
    public Task<Subscriber> CreateNewSubscriberAsync(CreateNewSubscriberRequest createNewSubscriberRequest);
}