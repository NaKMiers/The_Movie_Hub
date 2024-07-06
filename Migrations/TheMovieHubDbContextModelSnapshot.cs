﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using the_movie_hub.Models.Main;

#nullable disable

namespace the_movie_hub.Migrations
{
    [DbContext(typeof(TheMovieHubDbContext))]
    partial class TheMovieHubDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("the_movie_hub.Models.Main.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Banner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrailerUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.MovieGenre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoomTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TheaterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TheaterId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.RoomType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Seat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoomId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SeatColumn")
                        .HasColumnType("int");

                    b.Property<int>("SeatRow")
                        .HasColumnType("int");

                    b.Property<string>("SeatType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId1");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Showtime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TheaterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("RoomId");

                    b.HasIndex("RoomTypeId");

                    b.HasIndex("TheaterId");

                    b.ToTable("ShowTimes");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Theater", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Theaters");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MovieId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MovieId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoomId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SeatId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SeatId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TheaterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TheaterId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TicketTypeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TicketTypeId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("Total")
                        .HasColumnType("real");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MovieId1");

                    b.HasIndex("RoomId1");

                    b.HasIndex("SeatId1");

                    b.HasIndex("TheaterId1");

                    b.HasIndex("TicketTypeId1");

                    b.HasIndex("UserId1");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.TicketType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("TicketTypes");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Expended")
                        .HasColumnType("float");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.MovieGenre", b =>
                {
                    b.HasOne("the_movie_hub.Models.Main.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("the_movie_hub.Models.Main.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Room", b =>
                {
                    b.HasOne("the_movie_hub.Models.Main.Theater", null)
                        .WithMany("Rooms")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Seat", b =>
                {
                    b.HasOne("the_movie_hub.Models.Main.Room", "Room")
                        .WithMany("Seats")
                        .HasForeignKey("RoomId1");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Showtime", b =>
                {
                    b.HasOne("the_movie_hub.Models.Main.Movie", "Movie")
                        .WithMany("ShowTimes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("the_movie_hub.Models.Main.Room", null)
                        .WithMany("ShowTimes")
                        .HasForeignKey("RoomId");

                    b.HasOne("the_movie_hub.Models.Main.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("the_movie_hub.Models.Main.Theater", "Theater")
                        .WithMany("ShowTimes")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("RoomType");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Ticket", b =>
                {
                    b.HasOne("the_movie_hub.Models.Main.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId1");

                    b.HasOne("the_movie_hub.Models.Main.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId1");

                    b.HasOne("the_movie_hub.Models.Main.Seat", "Seat")
                        .WithMany("Tickets")
                        .HasForeignKey("SeatId1");

                    b.HasOne("the_movie_hub.Models.Main.Theater", "Theater")
                        .WithMany()
                        .HasForeignKey("TheaterId1");

                    b.HasOne("the_movie_hub.Models.Main.TicketType", "TicketType")
                        .WithMany()
                        .HasForeignKey("TicketTypeId1");

                    b.HasOne("the_movie_hub.Models.Main.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId1");

                    b.Navigation("Movie");

                    b.Navigation("Room");

                    b.Navigation("Seat");

                    b.Navigation("Theater");

                    b.Navigation("TicketType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Movie", b =>
                {
                    b.Navigation("MovieGenres");

                    b.Navigation("ShowTimes");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Room", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("ShowTimes");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Seat", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.Theater", b =>
                {
                    b.Navigation("Rooms");

                    b.Navigation("ShowTimes");
                });

            modelBuilder.Entity("the_movie_hub.Models.Main.User", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
