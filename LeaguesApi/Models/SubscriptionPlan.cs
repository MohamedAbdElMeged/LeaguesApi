using System.ComponentModel.DataAnnotations;

namespace LeaguesApi.Models;

public class SubscriptionPlan
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    public int Quota { get; set; }
    
}