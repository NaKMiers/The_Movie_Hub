namespace the_movie_hub.Models.Main;

public partial class Movie
{
    public required Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public string? Content { get; set; }

    public string Director { get; set; } = null!;

    public string? Actors { get; set; }

    public int Duration { get; set; }

    public string TrailerUrl { get; set; } = null!;

    public decimal Rating { get; set; }

    public string? Country { get; set; }

    public string? Note { get; set; }

    public string? Banner { get; set; }

    public string Img { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
