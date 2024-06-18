using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Genre
{
   public class GenreModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;

      // Properties
      public IEnumerable<Models.Main.Genre> Genres { get; set; } = [];

      // Methods
      public void OnGet()
      {
         Genres = [.. db.Genres];
      }

      public void OnPostDelete(string Id)
      {
         var theater = db.Genres.FirstOrDefault(t => t.Id.ToString() == Id);
         if (theater != null)
         {
            db.Genres.Remove(theater);
            db.SaveChanges();
         }

         // redirect to the theaters page
         Response.Redirect("/Admin/Genre");
      }
   }
}