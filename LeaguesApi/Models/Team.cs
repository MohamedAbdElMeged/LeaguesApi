using System.ComponentModel.DataAnnotations;

namespace LeaguesApi.Models;

public class Team
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public int LeagueId { get; set; }
    public virtual League League { get; set; }
}