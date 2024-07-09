using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Showtime
{
  public class EditShowtimeModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [FromRoute]
    public required string Id { get; set; }

    public IEnumerable<Models.Main.Movie> Movies { get; set; } = [];

    public IEnumerable<Models.Main.Theater> Theaters { get; set; } = [];

    public IEnumerable<Models.Main.Room> Rooms { get; set; } = [];

    [FromQuery]
    public required string TheaterId { get; set; }

    [BindProperty]
    public required string MovieId { get; set; }

    [BindProperty]
    public required string RoomId { get; set; }

    [BindProperty]
    public required DateTime StartAt { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

    // Methods
    public void OnGet()
    {
      // Get the showtime
      var showtime = db.ShowTimes.FirstOrDefault(t => t.Id.ToString() == Id);

      // check if the showtime is not null
      if (showtime != null)
      {
        // set the properties
        MovieId = showtime.MovieId.ToString();
        RoomId = showtime.RoomId.ToString();
        StartAt = showtime.StartAt;
      }

      // Get all movies is active
      Movies = db.Movies.Where(m => m.Active == true);

      // Get all theaters
      Theaters = db.Theaters;

      // Get all rooms and group by theater
      Rooms = db.Rooms.Where(r => r.TheaterId.ToString() == TheaterId).Include(r => r.RoomType);
    }

    public void OnPost()
    {
      // edit showtime
      var showtime = db.ShowTimes.FirstOrDefault(t => t.Id.ToString() == Id);
      if (showtime != null)
      {
        showtime.TheaterId = Guid.Parse(TheaterId);
        showtime.MovieId = Guid.Parse(MovieId);
        showtime.RoomId = Guid.Parse(RoomId);
        showtime.StartAt = StartAt;

        db.SaveChanges();
      }

      // redirect to all show times pages
      Response.Redirect($"/Admin/Showtime");
    }
  }
}