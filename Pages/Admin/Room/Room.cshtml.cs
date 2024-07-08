using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Room
{
   public class RoomModel(TheMovieHubDbContext db) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;

      // Properties
      [FromRoute]
      public required string TheaterId { get; set; }
      public Models.Main.Theater? Theater { get; set; }
      public IEnumerable<Models.Main.Room> Rooms { get; set; } = [];

      // Methods
      public void OnGet()
      {
         // get theater
         Theater = db.Theaters.Find(Guid.Parse(TheaterId));
         if (Theater == null)
         {
            // redirect to the theaters page
            Response.Redirect($"/Admin/Room/{TheaterId}");
            return;
         }

         // get theater's rooms include roomType
         Rooms = db.Rooms
            .Include(t => t.RoomType)
            .Where(t => t.TheaterId == Theater.Id);
      }

      public void OnPostDelete(string Id)
      {
         // get room to delete
         var room = db.Rooms.FirstOrDefault(t => t.Id.ToString() == Id);

         // check if room is null
         if (room == null)
         {
            Response.Redirect($"/Admin/Room/{TheaterId}");
            return;
         }

         // mustn't delete room if there are any seats are not available
         var notAvailableSeats = db.Seats.Where(s => s.RoomId.ToString() == Id && s.IsAvailable == true);
         if (notAvailableSeats.Any())
         {
            Response.Redirect($"/Admin/Room/{TheaterId}");
            return;
         }

         // delete room
         db.Rooms.Remove(room);

         // update room amount in theater
         var theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == TheaterId);
         if (theater != null)
         {
            theater.RoomAmount--;
            db.SaveChanges();
         }

         // redirect to the rooms page
         Response.Redirect($"/Admin/Room/{TheaterId}");
         return;
      }
   }
}