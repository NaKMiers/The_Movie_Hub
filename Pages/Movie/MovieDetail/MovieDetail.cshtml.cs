using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Movie.MovieDetail
{
  public class ShowtimeViewModel
  {
    public DateTime Date { get; set; }
    public List<TheaterViewModel> Theaters { get; set; } = new();
  }

  public class TheaterViewModel
  {
    public required string Name { get; set; }
    public required string Address { get; set; }
    public Dictionary<string, List<string>> RoomTypeShowTimes { get; set; } = [];
  }

  public class MovieDetailModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [FromRoute]
    public required string Id { get; set; }
    public Models.Main.Movie? Movie { get; set; }

    public IEnumerable<Showtime> ShowTimes { get; set; } = [];

    public IEnumerable<TheaterViewModel> Theaters { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string? Date { get; set; }

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
          .Where(s => s.MovieId.ToString() == Id)
          .OrderBy(s => s.StartAt);

      if (DateTime.TryParseExact(Date, "dd-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDate))
      {
        // group showTimes
        var showTimes = db.ShowTimes
            .Where(s => s.MovieId.ToString() == Id && s.StartAt.Date == selectedDate.Date)
            .OrderBy(s => s.StartAt)
            .Include(s => s.Theater)
            .Include(s => s.RoomType)
            .ToList();

        Theaters = showTimes
            .GroupBy(s => s.Theater)
            .Select(g => new TheaterViewModel
            {
              Name = g.Key.Name,
              Address = g.Key.Address,
              RoomTypeShowTimes = g.GroupBy(r => r.RoomType.Title)
                    .ToDictionary(rt => rt.Key, rt => rt.Select(s => s.StartAt.ToString("HH:mm")).ToList())
            }).ToList();
      }

    }
  }
}

