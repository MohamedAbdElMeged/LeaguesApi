using LeaguesApi.Data;
using LeaguesApi.Dtos.Requests;
using LeaguesApi.Dtos.Responses;
using LeaguesApi.Exceptions;
using LeaguesApi.Mappers;
using LeaguesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi.Services;


public class LeagueService : ILeagueService
{
    private readonly ApplicationDbContext _context;

    public LeagueService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<League> GetLeagueById(int id)
    {
      var league =  await _context.Leagues.FirstOrDefaultAsync(l => l.Id == id);
      if (league == null)
          throw new NotFoundException($"League with id {id} not found.");
      return league;
    }

    public async Task<League> CreateLeague(CreateLeagueRequest createLeagueRequest)
    {
        var league = createLeagueRequest.ToLeagueFromCreateLeagueDto();
        await _context.Leagues.AddAsync(league);
        await _context.SaveChangesAsync();
        return league;
    }

    public async Task<List<League>> GetAllLeagues(LeaguesFilterRequest leaguesFilterRequest)
    {
        IQueryable <League> query = _context.Leagues.AsQueryable();
        if (!string.IsNullOrEmpty(leaguesFilterRequest.Name))
        {
            query = query.Where(l => l.Name == leaguesFilterRequest.Name);
        }
        var totalCount = await query.CountAsync();
        var leagues  = await query
            .Skip((leaguesFilterRequest.Page - 1) * leaguesFilterRequest.PageSize)
            .Take(leaguesFilterRequest.PageSize) 
            .ToListAsync();
        return leagues;
    }

    public async Task<League> UpdateLeague(int leagueId, CreateLeagueRequest createLeagueRequest)
    {
        var league = await _context.Leagues.FirstOrDefaultAsync(l => l.Id == leagueId);
        if (league == null)
        {
            throw new NotFoundException($"League with id {leagueId} not found.");
        }

        league.Name = createLeagueRequest.Name;
        _context.Leagues.Update(league);
        await _context.SaveChangesAsync();
        return league;
    }

    public async Task<League> DeleteLeague(int id)
    {
        var league = await GetLeagueById(id);
        var subscriptionExist =  _context.Subscriptions.Where(s => s.Season.Id == id).Count();
        if (subscriptionExist > 0)
        {
            throw new BadRequestException("League Has Active Subscriptions");
        }

        _context.Leagues.Remove(league);
        await _context.SaveChangesAsync();
        return league;
    }
}