namespace the_movie_hub.Models.Main;

public partial class TicketType
{
  public required Guid Id { get; set; }

  public required string Label { get; set; }

  public required Guid RoomTypeId { get; set; }

  public required float Price { get; set; }

  public string? Description { get; set; }

  public virtual RoomType? RoomType { get; set; }
}
