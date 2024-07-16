namespace the_movie_hub.Models.Main;

public partial class Ticket
{
    public Guid Id { get; set; } = new Guid();

    public required string Email { get; set; }

    public required Guid MovieId { get; set; }

    public required Guid TheaterId { get; set; }

    public required Guid TicketTypeId { get; set; }

    public required Guid RoomId { get; set; }

    public required Guid SeatId { get; set; }

    public DateTime? StartAt { get; set; }

    public required float Total { get; set; }

    public required string PaymentMethod { get; set; }

    public string? Status { get; set; }

    public virtual TicketType? TicketType { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Theater? Theater { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Seat? Seat { get; set; }

}
