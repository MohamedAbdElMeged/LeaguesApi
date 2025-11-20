using LeaguesApi.Data;
using LeaguesApi.Dtos.Requests;
using LeaguesApi.Exceptions;
using LeaguesApi.Mappers;
using LeaguesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi.Services;

public class SeasonService : ISeasonService
{
    private readonly ApplicationDbContext _context;

    public SeasonService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Season> GetSeasonById(int seasonId)
    {
        var season = await _context.Seasons.FirstOrDefaultAsync(s => s.Id == seasonId);
        if (season == null)
        {
            throw new NotFoundException($"Season with {seasonId} not found");
        }

        return season;
    }

    public async Task<Season> CreateSeason(CreateSeasonRequest createSeasonRequest)
    {
        var season = createSeasonRequest.ToSeasonEntity();
        await _context.Seasons.AddAsync(season);
        await _context.SaveChangesAsync();
        return season;
    }
}