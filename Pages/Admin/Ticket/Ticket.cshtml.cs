using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Ticket
{
  public class TicketModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties 
    public IEnumerable<Models.Main.Ticket> Tickets { get; set; } = [];
    public List<GroupedTickets> GroupedTickets { get; set; } = [];

    // Methods

    public void OnGet()
    {
      var session = HttpContext.Session.GetString("User");
      if (session == null)
      {
        Response.Redirect("/Account/Login");
        return;
      }

      var user = session != null ? JsonConvert.DeserializeObject<Models.Main.User>(session) : null;
      if (user == null)
      {
        Response.Redirect("/Account/Login");
        return;
      }

      // Get all tickets include movie, theater and group by CreatedAt
      GroupedTickets = [.. db.Tickets
                .Include(t => t.Movie)
                .Include(t => t.Theater)
                .Include(t => t.TicketType)
                .Include(t => t.Room)
                .Include(t => t.Seat)
                .AsEnumerable()
                .GroupBy(t => t.CreatedAt.Date)
                .Select(g => new GroupedTickets
                {
                    CreatedAt = g.Key,
                    Tickets = [.. g]
                })
                .OrderByDescending(g => g.CreatedAt)];
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

  public class GroupedTickets
  {
    public DateTime CreatedAt { get; set; }
    public List<Models.Main.Ticket> Tickets { get; set; } = [];
  }
}