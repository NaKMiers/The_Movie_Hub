using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Seat
{
   public class SeatModel(TheMovieHubDbContext db) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;

      // Properties
      [FromRoute]
      public required string RoomId { get; set; }
      public Models.Main.Room? Room { get; set; }
      public IEnumerable<Models.Main.Seat> Seats { get; set; } = [];

      // Methods
      public void OnGet()
      {
         // get room
         Room = db.Rooms.Find(Guid.Parse(RoomId));
         if (Room == null)
         {
            // redirect to the theaters page
            Response.Redirect($"/Admin/Seat/{RoomId}");
            return;
         }

         // get theater's Seats
         Seats = Seats = [.. db.Seats
            .Where(s => s.RoomId.ToString() == RoomId)
            .OrderBy(s => s.SeatRow)
            .ThenBy(s => s.SeatColumn)];
      }

      public void OnPostDelete(string Id)
      {
         var Seat = db.Seats.FirstOrDefault(t => t.Id.ToString() == Id);
         if (Seat != null)
         {
            db.Seats.Remove(Seat);

            // update Seat amount in theater
            var room = db.Rooms.FirstOrDefault(t => t.Id.ToString() == RoomId);
            if (room != null)
            {
               room.Capacity--;
               db.SaveChanges();
            }
         }

         // redirect to the Seats page
         Response.Redirect($"/Admin/Seat/{RoomId}");
      }
   }
}