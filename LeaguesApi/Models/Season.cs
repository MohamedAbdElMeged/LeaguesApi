using System.ComponentModel.DataAnnotations;

namespace LeaguesApi.Models;

public class Season
{
    [Key]
    public int Id { get; set; }

    public int Year { get; set; }
    public int LeagueId { get; set; }
    public virtual League League { get; set; }
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}

