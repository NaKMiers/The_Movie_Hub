using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Ticket
{
   public class TicketModel(TheMovieHubDbContext db) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;

      // Properties
      [FromRoute]
      public IEnumerable<Models.Main.Ticket> Tickets { get; set; } = [];

      // Methods
      public void OnGet()
      {
         Tickets = [.. db.Tickets];
      }
   }
}