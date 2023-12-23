using GameStoreWebApp.Domain.Entities.Addresses;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace GameStoreWebApp.Data.Contexts;

public class GameStoreDbContext : DbContext
{
    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
    {
    }

	public DbSet<User> Users { get; set; }
    public DbSet<Verification> Verifications { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Rate> Rates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Rates)
            .WithOne(r => r.User)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Feedbacks)
            .WithOne(f => f.User)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Game>()
            .HasMany<Rate>(g => g.Rates)
            .WithOne(r => r.Game)
            .OnDelete(DeleteBehavior.SetNull);

		base.OnModelCreating(modelBuilder);
	}
}
