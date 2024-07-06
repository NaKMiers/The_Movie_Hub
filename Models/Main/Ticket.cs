namespace the_movie_hub.Models.Main;

public partial class Ticket
{
    public required Guid Id { get; set; } = new Guid();

    public string? UserId { get; set; }

    public string? MovieId { get; set; }

    public string? TicketTypeId { get; set; }

    public string? TheaterId { get; set; }

    public string? RoomId { get; set; }

    public string? SeatId { get; set; }

    public DateTime? StartAt { get; set; }

    public float? Total { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }

    public virtual TicketType? TicketType { get; set; }

    public virtual User? User { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Theater? Theater { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Seat? Seat { get; set; }

}
