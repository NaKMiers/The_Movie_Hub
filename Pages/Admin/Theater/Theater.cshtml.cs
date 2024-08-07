using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Theater
{
   public class TheaterModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;
      private readonly IWebHostEnvironment environment = environment;

      // Properties
      public IEnumerable<Models.Main.Theater> Theaters { get; set; } = [];

      // Methods
      public void OnGet()
      {
         Theaters = [.. db.Theaters];
      }

      public void OnPostDelete(string Id)
      {
         // get theater to delete
         var theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == Id);

         // check if theater exists
         if (theater == null)
         {
            Response.Redirect("/Admin/Theater");
            return;
         }

         // not allow to delete theater if there are any rooms
         var rooms = db.Rooms.Where(room => room.TheaterId.ToString() == theater.Id.ToString());
         if (rooms.Any())
         {
            Response.Redirect("/Admin/Theater");
            return;
         }

         // remove theater's image
         if (theater.Image != null)
         {
            // remove old image
            if (System.IO.File.Exists(Path.Combine(environment.WebRootPath, "uploads", theater.Image)))
            {
               System.IO.File.Delete(Path.Combine(environment.WebRootPath, "uploads", theater.Image));
            }
         }

         // remove theater
         db.Theaters.Remove(theater);
         db.SaveChanges();

         // redirect to the theaters page
         Response.Redirect("/Admin/Theater");
      }
   }
}