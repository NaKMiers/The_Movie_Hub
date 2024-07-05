namespace the_movie_hub.Models.Main;

public partial class Showtime
{
    public required Guid Id { get; set; }

    public required Guid MovieId { get; set; }

    public required Guid TheaterId { get; set; }

    public required Guid RoomTypeId { get; set; }

    public DateTime StartAt { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual RoomType? RoomType { get; set; }

    public virtual Theater? Theater { get; set; }

}
