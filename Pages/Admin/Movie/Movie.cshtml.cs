using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Movie
{
   public class MovieModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;

      // Properties
      public IEnumerable<Models.Main.Movie> Movies { get; set; } = [];

      // Methods
      public void OnGet()
      {
         Movies = [.. db.Movies];
      }

      public void OnPostDelete(string Id)
      {
         var theater = db.Movies.FirstOrDefault(t => t.Id.ToString() == Id);
         if (theater != null)
         {
            db.Movies.Remove(theater);
            db.SaveChanges();
         }

         // redirect to the theaters page
         Response.Redirect("/Admin/Movie");
      }
   }
}