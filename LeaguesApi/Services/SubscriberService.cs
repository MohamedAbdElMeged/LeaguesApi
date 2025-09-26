using LeaguesApi.Data;
using LeaguesApi.Dtos;
using LeaguesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi.Services;


public class SubscriberService : ISubscriberService
{
    private readonly ApplicationDbContext _context;

    public SubscriberService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Subscriber> GetSubscriberAsync(string clientId, string clientSecret)
    {
        return await _context.Subscribers.FirstOrDefaultAsync(s => s.ClientId == clientId &&
                                                                   s.ClientSecret == clientSecret);
    }

    public async Task<Subscriber> GetSubscriberByName(string name)
    {
        return await _context.Subscribers.FirstOrDefaultAsync(s => s.Name == name);
    }

    public async Task<Subscriber> CreateNewSubscriberAsync(CreateNewSubscriberRequest createNewSubscriberRequest)
    {

        var subscriberExists = await GetSubscriberByName(createNewSubscriberRequest.Name);
        if (subscriberExists == null)
        {
            return null;
        }

        return new Subscriber();
    }
}