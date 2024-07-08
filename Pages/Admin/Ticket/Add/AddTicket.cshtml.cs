using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Ticket
{
  public class AddTicketModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [BindProperty]
    public required string UserId { get; set; }

    [BindProperty]
    public required string MovieId { get; set; }

    [BindProperty]
    public required string TicketTypeId { get; set; }

    [BindProperty]
    public required string TheaterId { get; set; }

    [BindProperty]
    public required string RoomId { get; set; }

    [BindProperty]
    public required string SeatId { get; set; }

    [BindProperty]
    public required DateTime StartAt { get; set; }

    [BindProperty]
    public required float Total { get; set; }

    [BindProperty]
    public required string PaymentMethod { get; set; }

    [BindProperty]
    public required string Status { get; set; }

    // data for testing
    public IEnumerable<Models.Main.User> Users { get; set; }
    public IEnumerable<Models.Main.Movie> Movies { get; set; }
    public IEnumerable<TicketType> TicketTypes { get; set; }
    public IEnumerable<Models.Main.Theater> Theaters { get; set; }


    public Models.Main.Seat[,]? SeatMatrix { get; set; }


    // Methods
    public void OnGet()
    {
      //
    }

  }
}