namespace the_movie_hub.Models.Main;

public partial class Theater
{
    public required Guid Id { get; set; }

    public required string Name { get; set; } = null!;

    public required string Address { get; set; } = null!;

    public string? Image { get; set; }

    public int? RoomAmount { get; set; }

    // public required string City { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = [];

    public virtual ICollection<Showtime> ShowTimes { get; set; } = [];
}
