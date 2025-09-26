using System.ComponentModel.DataAnnotations;

namespace LeaguesApi.Models;

public class Subscriber
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}