using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Home.Index
{
  public class IndexModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    public IEnumerable<Models.Main.Movie> OnGoingMovies { get; set; } = [];
    public IEnumerable<Models.Main.Movie> UpComingMovies { get; set; } = [];

    // Methods
    public void OnGet()
    {
      OnGoingMovies = db.Movies.Where(m => m.Active == true);
      UpComingMovies = db.Movies.Where(m => m.Active == false);
    }
  }
}