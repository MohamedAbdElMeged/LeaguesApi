using LeaguesApi.Models;
using LeaguesApi.Models.Enums;

namespace LeaguesApi.Data.Seeders;

public class ApplicationDbSeeder
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbSeeder(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public void Seed()
    {
        if (_context.Admins.ToList().Count == 0)
        {
            SeedAdmins();
        }
        if (_context.SubscriptionPlans.ToList().Count == 0)
        {
            SeedSubscriptionPlans();
        }
        if (_context.Leagues.ToList().Count == 0)
        {
            SeedLeaguesAndTeams();
            _context.SaveChanges();
        }
        if (_context.Matches.ToList().Count == 0)
        {
            SeedMatches();
            _context.SaveChanges();
        }

        


    }

    private void SeedAdmins()
    {
        var admin = new Admin()
        {
            Email = "admin@admin.com",
            Password = "123456"
        };
        _context.Admins.Add(admin);
        
    }

    private void SeedLeaguesAndTeams()
    {
        var league = new League()
        {
            Name = "English Premier League"
        };
        var teams = new List<Team>()
        {
            new Team()
            {
                Name = "Manchester City",
                League = league
            },
            new Team()
            {
                Name = "Liverpool",
                League = league
            },
            new Team()
            {
                Name = "Arsenal",
                League = league
            },
            new Team()
            {
                Name = "Manchester United",
                League = league
            },
            new Team()
            {
                Name = "Spurs",
                League = league
            }, 
            new Team()
            {
                Name = "Newcastle",
                League = league
            },
            new Team()
            {
                Name = "Chelsea",
                League = league
            },
        };
        _context.Leagues.Add(league);
        _context.Teams.AddRange(teams);
       
    }


    private void SeedSubscriptionPlans()
    {
        var subscriptionPlans = new List<SubscriptionPlan>()
        {
            new SubscriptionPlan()
            {
                Name = "Free",
                Quota = 10
            },
            new SubscriptionPlan()
            {
                Name = "Premium",
                Quota = 100
            },
            new SubscriptionPlan()
            {
                Name = "Platinum",
                Quota = 200
            },
        };
        _context.SubscriptionPlans.AddRange(subscriptionPlans);
    }

    private void SeedMatches()
    {
        var league = _context.Leagues.FirstOrDefault();
        var season = new Season()
        {
            League = league,
            Year = "2025"
        };
        _context.Seasons.Add(season);
        var matches = new List<Match>()
        {
            new Match()
            {
                Played = false,
                Season = season,
                MatchDate = DateTime.Today + TimeSpan.FromDays(10),
                MatchParticipations = new List<MatchParticipation>()
                {
                    new MatchParticipation()
                    {
                        Role = MatchRole.HOME,
                        Points = 0,
                        Score = 0,
                        Winner = false,
                        Team = _context.Teams.FirstOrDefault(t=> t.Name == "Chelsea")
                    },
                    new MatchParticipation()
                    {
                        Role = MatchRole.AWAY,
                        Points = 0,
                        Score = 0,
                        Winner = false,
                        Team = _context.Teams.FirstOrDefault(t=> t.Name == "Arsenal")
                    },
                }
            },
        };

        _context.Matches.AddRange(matches);
    }
}