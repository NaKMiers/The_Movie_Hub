using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
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

      public Models.Main.Seat[,]? SeatMatrix { get; set; }

      // Methods
      public void OnGet()
      {
         // get room include theater
         Room = db.Rooms.Include(r => r.Theater).FirstOrDefault(r => r.Id.ToString() == RoomId);

         // check if room is null
         if (Room == null)
         {
            // redirect to the theater page
            Response.Redirect($"/Admin/Theater");
            return;
         }

         // get seats
         Seats = db.Seats.Where(s => s.RoomId.ToString() == RoomId);

         if (Seats.Any())
         {
            // Get max row and column
            int maxRow = Seats.Max(s => s.SeatRow);
            int maxColumn = Seats.Max(s => s.SeatColumn);

            // Initialize seat matrix
            SeatMatrix = new Models.Main.Seat[maxRow + 1, maxColumn + 1];

            // Fill seat matrix
            foreach (var seat in Seats)
            {
               SeatMatrix[seat.SeatRow, seat.SeatColumn] = seat;
            }
         }
      }

      public void OnPostAddSeat(int col, int row, string type)
      {
         // check if seat already exists
         if (db.Seats.Any(s => s.RoomId.ToString() == RoomId && s.SeatRow == row && s.SeatColumn == col))
         {
            // redirect to the same page
            Response.Redirect($"/Admin/Seat/{RoomId}");
            return;
         }

         // create new seat
         var seat = new Models.Main.Seat
         {
            Id = Guid.NewGuid(),
            RoomId = RoomId,
            SeatRow = row,
            SeatColumn = col,
            SeatType = type,
            IsAvailable = true
         };

         db.Seats.Add(seat);
         db.SaveChanges();

         // redirect to the same page
         Response.Redirect($"/Admin/Seat/{RoomId}");
      }

      public void OnPostDeleteSeat(string seatId)
      {
         // get seat
         var seat = db.Seats.FirstOrDefault(s => s.Id.ToString() == seatId);

         if (seat != null && seat.IsAvailable == true)
         {
            db.Seats.Remove(seat);
            db.SaveChanges();
         }

         // redirect to the same page
         Response.Redirect($"/Admin/Seat/{RoomId}");
      }
   }
}