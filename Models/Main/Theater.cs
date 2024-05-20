using System;
using System.Collections.Generic;

namespace the_movie_hub.Models.Main;

public partial class Theater
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Image { get; set; }

    public int? RoomAmount { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
