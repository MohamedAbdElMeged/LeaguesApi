using System.ComponentModel.DataAnnotations;

namespace LeaguesApi.Models;

public class Team
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public int? SeasonId { get; set; }
    public virtual Season Season { get; set; }
    
}