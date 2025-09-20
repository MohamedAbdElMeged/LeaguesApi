using LeaguesApi.Models.Enums;

namespace LeaguesApi.Models;

public class MatchParticipation
{
    public int Id { get; set; }
    public MatchRole Role { get; set; }
    public int Score { get; set; }
    public int Points { get; set; }
    public int TeamId { get; set; }
    public virtual Team Team { get; set; }
    public int MatchId { get; set; }
    public virtual Match Match { get; set; }
    public bool Winner { get; set; }
}