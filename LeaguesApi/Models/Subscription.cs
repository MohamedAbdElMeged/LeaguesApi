namespace LeaguesApi.Models;

public class Subscription
{
    public int Id { get; set; }

    public DateTime SubscriptionDate { get; set; }
    public int RemainingQuota { get; set; }
    public int SeasonId { get; set; }
    public virtual Season Season { get; set; }
    public int SubscriptionPlanId { get; set; }
    public virtual SubscriptionPlan SubscriptionPlan { get; set; }
    public int SubscriberId { get; set; }
    public virtual Subscriber Subscriber { get; set; }
    public bool IsActive { get; set; }
    public Subscription()
    {
        SubscriptionDate = DateTime.Now;
        IsActive = true;
    }
    
}