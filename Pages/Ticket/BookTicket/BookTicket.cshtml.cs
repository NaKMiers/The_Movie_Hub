using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

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
    public required string? Id { get; set; }
    public Theater? Theater { get; set; }
    public List<MovieShowtimeGroup> MovieShowTimeGroups { get; set; } = [];

    public void OnGet()
    {
      // Get theater
      Theater = _db.Theaters.FirstOrDefault(t => t.Id.ToString() == Id);

      if (Theater != null)
      {
        // Get all today's show times of the theater
        var today = DateTime.Today;
        var showTimes = _db.ShowTimes
                           .Include(s => s.Movie)
                           .Include(s => s.Room).ThenInclude(r => r.RoomType)
                           .Where(s => s.TheaterId == Theater.Id && s.StartAt.Date == today)
                           .ToList();

        Console.WriteLine("showTimes.Count: " + showTimes.Count);

        // Group show times by movie and room type
        var movieGroups = showTimes
                            .GroupBy(s => s.Movie)
                            .Select(g => new MovieShowtimeGroup
                            {
                              Movie = g.Key,
                              RoomTypeShowTimes = g.GroupBy(s => s.Room!.RoomType!.Title)
                                                    .ToDictionary(
                                                        grp => grp.Key,
                                                        grp => grp.OrderBy(s => s.StartAt).ToList()
                                                    )
                            })
                            .ToList();

        MovieShowTimeGroups = movieGroups;

        Console.WriteLine("movieGroups.Count: " + movieGroups.Count);
        // loop through movieGroups
        foreach (var movieGroup in movieGroups)
        {
          Console.WriteLine("movieGroup.Movie.Title: " + movieGroup.Movie.Title);
          var roomTypeShowTimes = new Dictionary<string, List<Showtime>>();

          // loop through show times of the movie
          foreach (var roomTypeShowTime in movieGroup.RoomTypeShowTimes)
          {
            Console.WriteLine("roomTypeShowTime.Key: " + roomTypeShowTime.Key);
            Console.WriteLine("roomTypeShowTime.Value.Count: " + roomTypeShowTime.Value.Count);

            // loop through show times of the room type
            foreach (var showTime in roomTypeShowTime.Value)
            {
              Console.WriteLine("showTime.StartAt: " + showTime.StartAt);
            }
          }
        }
      }
    }
  }

  public class MovieShowtimeGroup
  {
    public Models.Main.Movie Movie { get; set; }
    public Dictionary<string, List<Showtime>> RoomTypeShowTimes { get; set; }
  }
}
