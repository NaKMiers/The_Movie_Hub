namespace the_movie_hub.Models.Main;

public partial class MovieGenre
{
    public required Guid Id { get; set; }

    public required Guid MovieId { get; set; }

    public required Guid GenreId { get; set; }

    public virtual Movie Movie { get; set; }

    public virtual Genre Genre { get; set; }
}