using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

         // get theater's rooms
         Rooms = db.Rooms.Where(r => r.TheaterId.ToString() == TheaterId);
      }

      public async Task OnPostAsync(string NextRoom)
      {
         // add new room to the theater
         var room = new Models.Main.Room
         {
            Id = Guid.NewGuid(),
            TheaterId = Guid.Parse(TheaterId),
            Name = NextRoom
         };
         db.Rooms.Add(room);
         db.SaveChanges();



         Response.Redirect($"/Admin/Room/{TheaterId}");
      }

      public void OnPostDelete(string Id)
      {
         var room = db.Rooms.FirstOrDefault(t => t.Id.ToString() == Id);
         if (room != null)
         {
            db.Rooms.Remove(room);

            // update room amount in theater
            var theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == TheaterId);
            if (theater != null)
            {
               theater.RoomAmount--;
               db.SaveChanges();
            }
         }

         // redirect to the rooms page
         Response.Redirect($"/Admin/Room/{TheaterId}");
      }
   }
}