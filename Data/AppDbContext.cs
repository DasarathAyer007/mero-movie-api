using mero_movie_api.Model;
using Microsoft.EntityFrameworkCore;

namespace mero_movie_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<Collection> Collections { get; set; }
    
    public DbSet<Rating> Ratings { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<WatchList> WatchLists { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WatchList>()
            .Property(x => x.Status)
            .HasConversion<int>()
            .HasDefaultValue(WatchStatus.PlanToWatch);

        modelBuilder.Entity<WatchList>()
            .HasIndex(x => new { x.UserId, x.MovieId })
            .IsUnique();
    }
    
}