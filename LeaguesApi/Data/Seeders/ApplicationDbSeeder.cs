using LeaguesApi.Models;
using LeaguesApi.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace LeaguesApi.Data.Seeders;

public class ApplicationDbSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<Admin> _passwordHasher;

    public ApplicationDbSeeder(ApplicationDbContext context, IPasswordHasher<Admin> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
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
            Email = "admin@admin.com"
            
        };
        admin.Password = _passwordHasher.HashPassword(admin, "123456");
        _context.Admins.Add(admin);
        
    }

    private void SeedLeaguesAndTeams()
    {
        
        var league = new League()
        {
            Name = "English Premier League"
        };
        var season = new Season()
        {
            League = league,
            Year = 2025
        };
        _context.Leagues.Add(league);
        _context.Seasons.Add(season);
        var teams = new List<Team>()
        {
            new Team()
            {
                Name = "Manchester City",
                Season = season
            },
            new Team()
            {
                Name = "Liverpool",
                Season = season
            },
            new Team()
            {
                Name = "Arsenal",
                Season = season
            },
            new Team()
            {
                Name = "Manchester United",
                Season = season
            },
            new Team()
            {
                Name = "Spurs",
                Season = season
            }, 
            new Team()
            {
                Name = "Newcastle",
                Season = season
            },
            new Team()
            {
                Name = "Chelsea",
                Season = season
            },
            new Team()
            {
                Name = "Al Ahly Sc"
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
        var season = _context.Seasons.FirstOrDefault();
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