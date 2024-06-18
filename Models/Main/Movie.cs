namespace the_movie_hub.Models.Main;

public partial class Movie
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public required string Content { get; set; }

    public required string Director { get; set; }

    public required string Actors { get; set; }

    public required int Duration { get; set; }

    public string? TrailerUrl { get; set; }

    public decimal? Rating { get; set; }

    public string? Country { get; set; }

    public string? Note { get; set; }

    public string? Banner { get; set; }

    public string? Image { get; set; }

    public bool? Active { get; set; } = true;

    public virtual ICollection<Genre> Genres { get; set; } = [];

    public virtual ICollection<Showtime> ShowTimes { get; set; } = [];
}
