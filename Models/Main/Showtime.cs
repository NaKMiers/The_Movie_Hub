namespace the_movie_hub.Models.Main;

public partial class Showtime
{
    public required Guid Id { get; set; }

    public string? MovieId { get; set; }

    public string? RoomId { get; set; }

    public string? TheaterId { get; set; }

    public DateTime Time { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Theater? Theater { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
