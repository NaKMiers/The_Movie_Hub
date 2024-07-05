using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Home.Index
{
  public class IndexModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
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
      OnGoingMovies = [.. OnGoingMovies, .. OnGoingMovies, .. OnGoingMovies, .. OnGoingMovies, .. OnGoingMovies, .. OnGoingMovies,];
      UpComingMovies = db.Movies.Where(m => m.Active == false);
      UpComingMovies = [.. UpComingMovies, .. UpComingMovies, .. UpComingMovies, .. UpComingMovies, .. UpComingMovies, .. UpComingMovies,];
    }
  }
}