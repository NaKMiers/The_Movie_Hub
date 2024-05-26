using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace the_movie_hub.Models.Main;

public partial class TheMovieHubDbContext : DbContext
{
    public TheMovieHubDbContext()
    {
    }

    public TheMovieHubDbContext(DbContextOptions<TheMovieHubDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieGenre> MovieGenres { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Showtime> Showtimes { get; set; }

    public virtual DbSet<Theater> Theaters { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-C8F8CM6;Database=TheMovieHub;MultipleActiveResultSets=true;User ID=admin;Password=asdasd;Trusted_Connection=True;TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__3213E83FF8C772EC");

            entity.ToTable("Genre");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movie__3213E83F7B4B077F");

            entity.ToTable("Movie");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Actors).HasColumnName("actors");
            entity.Property(e => e.Banner)
                .HasMaxLength(255)
                .HasColumnName("banner");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Director)
                .HasMaxLength(255)
                .HasColumnName("director");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("img");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(3, 1)")
                .HasColumnName("rating");
            entity.Property(e => e.ReleaseDate).HasColumnName("releaseDate");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.TrailerUrl)
                .HasMaxLength(255)
                .HasColumnName("trailerUrl");
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieGen__3213E83FF1F6377F");

            entity.ToTable("MovieGenre");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.GenreId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genreId");
            entity.Property(e => e.MovieId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("movieId");

            entity.HasOne(d => d.Genre).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__MovieGenr__genre__571DF1D5");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__MovieGenr__movie__5629CD9C");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room__3213E83FEB762314");

            entity.ToTable("Room");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TheaterId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("theaterId");

            entity.HasOne(d => d.Theater).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.TheaterId)
                .HasConstraintName("FK__Room__theaterId__3A81B327");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seat__3213E83F986DE9F6");

            entity.ToTable("Seat");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("isAvailable");
            entity.Property(e => e.RoomId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("roomId");
            entity.Property(e => e.SeatColumn).HasColumnName("seatColumn");
            entity.Property(e => e.SeatRow).HasColumnName("seatRow");
            entity.Property(e => e.SeatType)
                .HasMaxLength(50)
                .HasDefaultValue("local")
                .HasColumnName("seatType");

            entity.HasOne(d => d.Room).WithMany(p => p.Seats)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Seat__roomId__3D5E1FD2");
        });

        modelBuilder.Entity<Showtime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Showtime__3213E83F875868AA");

            entity.ToTable("Showtime");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.MovieId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("movieId");
            entity.Property(e => e.RoomId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("roomId");
            entity.Property(e => e.TheaterId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("theaterId");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");

            entity.HasOne(d => d.Movie).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__Showtime__movieI__47DBAE45");

            entity.HasOne(d => d.Room).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Showtime__roomId__48CFD27E");

            entity.HasOne(d => d.Theater).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.TheaterId)
                .HasConstraintName("FK__Showtime__theate__49C3F6B7");
        });

        modelBuilder.Entity<Theater>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Theater__3213E83F57659694");

            entity.ToTable("Theater");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.RoomAmount)
                .HasDefaultValue(0)
                .HasColumnName("roomAmount");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3213E83F0087E428");

            entity.ToTable("Ticket");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .HasColumnName("paymentMethod");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SeatId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("seatId");
            entity.Property(e => e.Showtimeid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("showtimeid");
            entity.Property(e => e.Userid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("userid");

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .HasConstraintName("FK__Ticket__seatId__5070F446");

            entity.HasOne(d => d.Showtime).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Showtimeid)
                .HasConstraintName("FK__Ticket__showtime__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__Ticket__userid__5165187F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F2F7D34B7");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Authtype)
                .HasMaxLength(50)
                .HasColumnName("authtype");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Expended)
                .HasDefaultValue(0.0)
                .HasColumnName("expended");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
