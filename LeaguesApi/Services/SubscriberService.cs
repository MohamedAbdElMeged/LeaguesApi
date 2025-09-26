using LeaguesApi.Data;
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

    public Task UpdateQuota(Subscriber subscriber)
    {
        throw new NotImplementedException();
    }
}