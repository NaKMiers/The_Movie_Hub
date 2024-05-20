using System;
using System.Collections.Generic;

namespace the_movie_hub.Models.Main;

public partial class Room
{
    public string Id { get; set; } = null!;

    public string? TheaterId { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();

    public virtual Theater? Theater { get; set; }
}
