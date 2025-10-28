using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LeaguesApi.Dtos.Requests;

public class CreateNewSubscriptionRequest
{
    [Required]
    public int SubscriptionPlanId { get; set; }
    [Required]
    public int SeasonId { get; set; }
    [JsonIgnore] 
    public int SubscriberId { get; set; }
}