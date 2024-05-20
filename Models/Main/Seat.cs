using System;
using System.Collections.Generic;

namespace the_movie_hub.Models.Main;

public partial class Seat
{
    public string Id { get; set; } = null!;

    public string? RoomId { get; set; }

    public int SeatRow { get; set; }

    public int SeatColumn { get; set; }

    public string? SeatType { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual Room? Room { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
