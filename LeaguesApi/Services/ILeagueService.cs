using LeaguesApi.Dtos.Requests;
using LeaguesApi.Models;

namespace LeaguesApi.Services;

public interface ILeagueService
{
    public Task<League> GetLeagueById(int id);
    public Task<League> CreateLeague(CreateLeagueRequest createLeagueRequest);
    public Task<List<League>> GetAllLeagues(LeaguesFilterRequest leaguesFilterRequest);
    public Task<League> UpdateLeague(int leagueId, CreateLeagueRequest createLeagueRequest);
    public Task<League> DeleteLeague(int id);
}