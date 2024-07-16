using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;
using the_movie_hub.Pages.Checkout;

namespace the_movie_hub.Pages.Ticket.BookTicket
{
  public class BookTicketModel : PageModel
  {
    private readonly TheMovieHubDbContext _db;

    public BookTicketModel(TheMovieHubDbContext db)
    {
      _db = db;
    }

    [FromRoute]
    public required string Id { get; set; }
    public Theater? Theater { get; set; }
    public List<MovieShowtimeGroup> MovieShowTimeGroups { get; set; } = [];

    [FromQuery]
    public string? Status { get; set; }

    public void OnGet()
    {
      Console.WriteLine("Theater Status: " + Status);

      // Get theater
      Theater = _db.Theaters.FirstOrDefault(t => t.Id.ToString() == Id);

      if (Theater != null)
      {
        // Get all show times of the theater
        var showTimes = new List<Showtime>();

        if (Status == "coming-soon")
        {
          showTimes = [.. _db.ShowTimes
                      .Include(s => s.Movie)
                      .Include(s => s.Room).ThenInclude(r => r.RoomType)
                      .Where(s => s.TheaterId == Theater.Id && s.Movie.ReleaseDate > DateOnly.FromDateTime(DateTime.Today) && s.Movie.Active == true)];
        }
        else
        {
          showTimes = [.. _db.ShowTimes
                      .Include(s => s.Movie)
                      .Include(s => s.Room).ThenInclude(r => r.RoomType)
                      .Where(s => s.TheaterId == Theater.Id && s.Movie.ReleaseDate <= DateOnly.FromDateTime(DateTime.Today) && s.Movie.Active == true)];
        }

        // Group show times by movie
        var movieGroups = showTimes
                            .GroupBy(s => s.Movie)
                            .Select(g => new MovieShowtimeGroup
                            {
                              Movie = g.Key,
                              DateGroups = g.GroupBy(s => s.StartAt.Date)
                                              .Select(dateGroup => new DateShowtimeGroup
                                              {
                                                Date = dateGroup.Key,
                                                RoomTypeShowTimes = dateGroup.GroupBy(s => s.Room!.RoomType!.Title)
                                                                               .ToDictionary(
                                                                                   roomGroup => roomGroup.Key,
                                                                                   roomGroup => roomGroup.OrderBy(s => s.StartAt).ToList()
                                                                               )
                                              })
                                              .ToList()
                            })
                            .ToList();

        MovieShowTimeGroups = movieGroups;

        // Debug output
        foreach (var movieGroup in movieGroups)
        {
          Console.WriteLine("Movie: " + movieGroup.Movie.Title);
          foreach (var dateGroup in movieGroup.DateGroups)
          {
            Console.WriteLine("  Date: " + dateGroup.Date.ToShortDateString());
            foreach (var roomTypeShowTime in dateGroup.RoomTypeShowTimes)
            {
              Console.WriteLine("    Room Type: " + roomTypeShowTime.Key);
              foreach (var showTime in roomTypeShowTime.Value)
              {
                Console.WriteLine("      Showtime: " + showTime.StartAt);
              }
            }
          }
        }
      }
    }
  }

  public class MovieShowtimeGroup
  {
    public Models.Main.Movie Movie { get; set; }
    public List<DateShowtimeGroup> DateGroups { get; set; } = new List<DateShowtimeGroup>();
  }

  public class DateShowtimeGroup
  {
    public DateTime Date { get; set; }
    public Dictionary<string, List<Showtime>> RoomTypeShowTimes { get; set; } = new Dictionary<string, List<Showtime>>();
  }
}
