using LeaguesApi.Models;

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
        if (_context.Leagues.ToList().Count == 0)
        {
            SeedLeaguesAndTeams();
        }
        _context.SaveChanges();
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
    
}