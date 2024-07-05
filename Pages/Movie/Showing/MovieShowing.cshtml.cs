using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Movie.MovieShowing
{
  public class MovieShowingModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    public IEnumerable<Models.Main.Movie> Movies { get; set; } = [];

    // Methods
    public void OnGet()
    {
      Movies = db.Movies.Where(m => m.Active == true);
    }
  }
}