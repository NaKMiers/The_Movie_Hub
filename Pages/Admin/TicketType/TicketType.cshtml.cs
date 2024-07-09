using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Ticket
{
   public class TicketTypeModel(TheMovieHubDbContext db) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;

      // Properties
      [FromRoute]
      public IEnumerable<TicketType> TicketTypes { get; set; } = [];

      public IEnumerable<Models.Main.RoomType> RoomTypes { get; set; } = [];

      // Methods
      public void OnGet()
      {
         TicketTypes = db.TicketTypes.Include(t => t.RoomType);
         RoomTypes = db.RoomTypes;
      }

      public void OnPostDelete(string Id)
      {
         var ticketType = db.TicketTypes.FirstOrDefault(t => t.Id.ToString() == Id);
         if (ticketType != null)
         {
            db.TicketTypes.Remove(ticketType);
            db.SaveChanges();
         }

         // redirect to the same page
         Response.Redirect("/Admin/TicketType");
      }
   }
}