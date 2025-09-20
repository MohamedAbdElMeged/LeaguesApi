namespace LeaguesApi.Models;

public class Match
{
    public int Id { get; set; }
    public bool Played { get; set; }
    public DateTime MatchDate { get; set; }
    public int SeasonId { get; set; }
    public virtual Season Season { get; set; }
}