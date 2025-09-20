using LeaguesApi.Data.Seeders;
using LeaguesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<MatchParticipation> MatchParticipations { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<Team> Teams { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<League>()
            .Property(s => s.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Team>()
            .Property(s => s.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Subscriber>()
            .HasIndex(e => new { e.ClientId })
            .IsUnique();
        modelBuilder.Entity<Subscriber>()
            .Property(e=>  e.ClientId)
            .IsRequired();
        modelBuilder.Entity<Subscriber>()
            .HasIndex(e => new { e.ClientSecret })
            .IsUnique();
        modelBuilder.Entity<Subscriber>()
            .Property(e=>  e.ClientSecret)
            .IsRequired();
        modelBuilder.Entity<Team>()
            .HasOne(t => t.League)
            .WithMany(l => l.Teams)
            .HasForeignKey(t => t.LeagueId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}