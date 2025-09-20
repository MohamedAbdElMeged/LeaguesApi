using System.ComponentModel.DataAnnotations;

namespace LeaguesApi.Models;

public class Season
{
    [Key]
    public int Id { get; set; }

    public int Year { get; set; }
}