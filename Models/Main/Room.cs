namespace the_movie_hub.Models.Main;

public partial class Room
{
    public required Guid Id { get; set; }

    public string? TheaterId { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Showtime> ShowTimes { get; set; } = new List<Showtime>();

    public virtual Theater? Theater { get; set; }
}
