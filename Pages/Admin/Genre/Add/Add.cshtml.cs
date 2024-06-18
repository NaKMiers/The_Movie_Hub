using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Genre
{
   public class AddGenreModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;
      public required IEnumerable<Models.Main.Genre> Genres { get; set; }

      // Properties
      public void OnGet()
      {
         Genres = [.. db.Genres];
      }

      // Methods
      public async Task OnPostAsync(Models.Main.Genre genre)
      {
         db.Genres.Add(genre);
         await db.SaveChangesAsync();
      }
   }
}