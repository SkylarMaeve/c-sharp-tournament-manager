namespace PV178_Project.Data;

using PV178_Project.Models;
using Microsoft.EntityFrameworkCore;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
    {
    }

    public DbSet<Sport> Sport { get; set; }
    public DbSet<Tournament> Tournament { get; set; }
    public DbSet<Team> Team { get; set; }
    public DbSet<Match> Match { get; set; }
    public DbSet<Player> Player { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tournament>()
            .HasOne(t => t.Sport)
            .WithMany()
            .HasForeignKey("SportId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Team>()
            .HasOne<Tournament>(t => t.Tournament)
            .WithMany()
            .HasForeignKey("TournamentId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Player>()
            .HasOne<Team>(p => p.Team)
            .WithMany()
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Match>()
            .HasOne<Tournament>(m => m.Tournament)
            .WithMany()
            .HasForeignKey("TournamentId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Match>()
            .HasOne<Team>(m => m.TeamA)
            .WithMany()
            .HasForeignKey("TeamAId")
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Match>()
            .HasOne<Team>(m => m.TeamB)
            .WithMany()
            .HasForeignKey("TeamBId")
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Match>()
            .HasOne<Team>(m => m.Winner)
            .WithMany()
            .HasForeignKey("WinnerId")
            .OnDelete(DeleteBehavior.SetNull);
    }
}