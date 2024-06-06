namespace the_movie_hub.Models.Main;

public partial class Theater
{
    public required Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Image { get; set; }

    public int? RoomAmount { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Showtime> ShowTimes { get; set; } = new List<Showtime>();
}
