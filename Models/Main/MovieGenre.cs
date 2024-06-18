namespace the_movie_hub.Models.Main;

public partial class MovieGenre
{
    public required Guid Id { get; set; }

    public required Guid MovieId { get; set; }

    public required Guid GenreId { get; set; }
}