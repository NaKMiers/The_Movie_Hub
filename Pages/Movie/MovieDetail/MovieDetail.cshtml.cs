using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Movie.MovieDetail
{
  public class MovieDetailModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [FromRoute]
    public required string Id { get; set; }
    public Models.Main.Movie? Movie { get; set; }

    public IEnumerable<Showtime> ShowTimes { get; set; } = [];

    public IEnumerable<Showtime> ShowTimesByDate { get; set; } = [];

    public IEnumerable<string> Cities { get; set; } = [];

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

      // get showTimes
      ShowTimes = db.ShowTimes
          .Include(s => s.Theater)
          .Where(s => s.MovieId.ToString() == Id)
          .OrderBy(s => s.StartAt);

      // group showTimes by date
      ShowTimesByDate = ShowTimes
          .GroupBy(s => s.StartAt.Date)
          .Select(g => g.First());

      // get theaters from showTimes then group by city
      Cities = ShowTimes
          .Select(s => s.Theater.City)
          .Distinct()
          .OrderBy(c => c);
    }
  }
}

