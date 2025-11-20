using LeaguesApi.Dtos.Requests;
using LeaguesApi.Models;

namespace LeaguesApi.Services;

public interface ISeasonService
{
    public Task<Season> GetSeasonById(int seasonId);
    public Task<Season> CreateSeason(CreateSeasonRequest createSeasonRequest);
    // public Task<List<Season>> GetAll();

}