namespace the_movie_hub.Models.Main;

public partial class Room
{
    public required Guid Id { get; set; }

    public required Guid TheaterId { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = [];

    public virtual ICollection<Showtime> ShowTimes { get; set; } = [];
}
