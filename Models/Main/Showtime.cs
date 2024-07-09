namespace the_movie_hub.Models.Main;

public partial class Showtime
{
    public required Guid Id { get; set; }

    public required Guid MovieId { get; set; }

    public required Guid TheaterId { get; set; }

    public required Guid RoomId { get; set; }

    public DateTime StartAt { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Theater? Theater { get; set; }

}
