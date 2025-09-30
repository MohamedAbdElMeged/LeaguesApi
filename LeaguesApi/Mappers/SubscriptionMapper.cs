using AutoMapper;
using LeaguesApi.Dtos;
using LeaguesApi.Models;

namespace LeaguesApi.Mappers;

public class SubscriptionMapper: Profile
{
    public SubscriptionMapper()
    {

        CreateMap<Subscription, SubscriptionResponse>();
    }
}