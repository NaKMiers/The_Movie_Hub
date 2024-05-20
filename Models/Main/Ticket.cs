using System;
using System.Collections.Generic;

namespace the_movie_hub.Models.Main;

public partial class Ticket
{
    public string Id { get; set; } = null!;

    public string? Showtimeid { get; set; }

    public string? SeatId { get; set; }

    public string? Userid { get; set; }

    public decimal Price { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Seat? Seat { get; set; }

    public virtual Showtime? Showtime { get; set; }

    public virtual User? User { get; set; }
}
