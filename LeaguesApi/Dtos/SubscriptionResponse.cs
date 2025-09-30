namespace LeaguesApi.Dtos;

public class SubscriptionResponse
{
    public int Id { get; set; }
    public int SeasonId { get; set; }
    public int SubscriptionPlanId { get; set; }
    public int SubscriberId { get; set; }
    public DateTime SubscriptionDate { get; set; }
    public int RemainingQuota { get; set; }
}