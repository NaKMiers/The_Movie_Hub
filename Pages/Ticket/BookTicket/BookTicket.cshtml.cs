using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Ticket.BookTicket
{
  public class BookTicketModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [FromRoute]
    public required string? Id { get; set; }
    public Theater? Theater { get; set; }

    // Methods
    public void OnGet()
    {
      // get theater
      Theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == Id);
    }
  }
}