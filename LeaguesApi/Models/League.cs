using System.ComponentModel.DataAnnotations;

namespace LeaguesApi.Models;

public class League
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}