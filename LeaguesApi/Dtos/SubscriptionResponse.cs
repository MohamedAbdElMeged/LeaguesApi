namespace LeaguesApi.Dtos;

public class SubscriptionResponse
{
    public int Id { get; set; }
    public int SeasonId { get; set; }
    public SubscriptionPlanResponse SubscriptionPlan { get; set; }
    public DateTime SubscriptionDate { get; set; }
    public int RemainingQuota { get; set; }
    public bool IsActive { get; set; }
}