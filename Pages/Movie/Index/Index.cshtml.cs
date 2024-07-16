using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Movie.Index
{
  public class MovieIndexModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    public IEnumerable<Models.Main.Movie> OnGoingMovies { get; set; } = [];
    public IEnumerable<Models.Main.Movie> UpComingMovies { get; set; } = [];

    // Methods
    public void OnGet()
    {
      // Get on going movies (active = true, release date <= today)
      OnGoingMovies = db.Movies.Where(m => m.Active == true && m.ReleaseDate <= DateOnly.FromDateTime(DateTime.Today));

      // Get on upcoming movies (active = true, release date > today)
      UpComingMovies = db.Movies.Where(m => m.Active == true && m.ReleaseDate > DateOnly.FromDateTime(DateTime.Today));
    }
  }
}