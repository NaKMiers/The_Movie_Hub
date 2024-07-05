using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Movie.MovieDetail
{
  public class MovieDetailModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [FromRoute]
    public required string Id { get; set; }
    public Models.Main.Movie? Movie { get; set; }

    // Methods
    public void OnGet()
    {
      // get movie and genres
      Movie = db.Movies
        .Include(m => m.MovieGenres)
        .ThenInclude(mg => mg.Genre)
        .FirstOrDefault(m => m.Id.ToString() == Id);

      if (Movie == null)
      {
        // redirect to the theaters page
        Response.Redirect("/");
        return;
      }
    }
  }
}