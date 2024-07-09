using Microsoft.EntityFrameworkCore;

namespace the_movie_hub.Models.Main;

public partial class TheMovieHubDbContext : DbContext
{
    public TheMovieHubDbContext(DbContextOptions<TheMovieHubDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieGenre> MovieGenres { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Showtime> ShowTimes { get; set; }

    public virtual DbSet<Theater> Theaters { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Showtime>()
            .HasOne(s => s.Theater)
            .WithMany(t => t.ShowTimes)
            .HasForeignKey(s => s.TheaterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
