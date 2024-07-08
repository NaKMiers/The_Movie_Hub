using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Ticket
{
  public class TicketModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    public IEnumerable<Models.Main.Ticket> Tickets { get; set; } = [];

    // Methods
    public void OnGet()
    {
      // get all tickets include movie, theater
      Tickets = db.Tickets.Include(t => t.Movie).Include(t => t.Theater);
    }

    public void OnPostDelete(string Id)
    {
      var ticket = db.Tickets.FirstOrDefault(t => t.Id.ToString() == Id);
      if (ticket != null)
      {
        db.Tickets.Remove(ticket);
        db.SaveChanges();
      }

      // redirect to the same page
      Response.Redirect("/Admin/Ticket");
    }
  }
}