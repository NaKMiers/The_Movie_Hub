namespace the_movie_hub.Models.Main;

public partial class MovieGenre
{
    public required Guid Id { get; set; }

    public string? MovieId { get; set; }

    public string? GenreId { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual Movie? Movie { get; set; }
}
