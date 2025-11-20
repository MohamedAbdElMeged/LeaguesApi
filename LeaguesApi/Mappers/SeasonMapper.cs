using LeaguesApi.Dtos.Requests;
using LeaguesApi.Dtos.Responses;
using LeaguesApi.Models;

namespace LeaguesApi.Mappers;

public static class SeasonMapper
{
    public static Season ToSeasonEntity(this CreateSeasonRequest createSeasonRequest)
    {
        return new Season()
        {
            Year = createSeasonRequest.Year,
            LeagueId = createSeasonRequest.LeagueId
        };
    }

    public static SeasonResponse ToSeasonResposeFromEntity(this Season season)
    {
        return new SeasonResponse()
        {
            Id = season.Id,
            LeagueId = season.LeagueId,
            Year = season.Year
        };
    }
}