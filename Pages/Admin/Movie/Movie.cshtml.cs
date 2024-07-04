using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
         // get all movie and include the genres
         Movies = [.. db.Movies.Include(movie => movie.MovieGenres).ThenInclude(movieGenre => movieGenre.Genre)];
      }

      public void OnPostDelete(string Id)
      {
         var movie = db.Movies.FirstOrDefault(t => t.Id.ToString() == Id);
         if (movie != null)
         {
            db.Movies.Remove(movie);
            db.SaveChanges();

            // remove the image
            if (movie.Image != null)
            {
               if (System.IO.File.Exists(Path.Combine(environment.WebRootPath, "uploads", movie.Image)))
               {
                  System.IO.File.Delete(Path.Combine(environment.WebRootPath, "uploads", movie.Image));
               }
            }

            // remove the banner
            if (movie.Banner != null)
            {
               if (System.IO.File.Exists(Path.Combine(environment.WebRootPath, "uploads", movie.Banner)))
               {
                  System.IO.File.Delete(Path.Combine(environment.WebRootPath, "uploads", movie.Banner));
               }
            }
         }

         // redirect to the movies page
         Response.Redirect("/Admin/Movie");
      }
   }
}