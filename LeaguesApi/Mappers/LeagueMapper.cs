using LeaguesApi.Dtos.Responses;
using LeaguesApi.Dtos.Requests;
using LeaguesApi.Models;

namespace LeaguesApi.Mappers;

public static class LeagueMapper
{
    public static League ToLeagueFromCreateLeagueDto(this CreateLeagueRequest createLeagueRequest)
    {
        return new League()
        {
            Name = createLeagueRequest.Name
        };
    }

    public static LeagueResponse ToLeagueResponse(this League league)
    {
        return new LeagueResponse()
        {
            Id = league.Id,
            Name = league.Name
        };
    }
}