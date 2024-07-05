using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public IEnumerable<Models.Main.RoomType> RoomTypes { get; set; } = [];

    [BindProperty]
    public required string TheaterId { get; set; }

    [BindProperty]
    public required string MovieId { get; set; }

    [BindProperty]
    public required string RoomTypeId { get; set; }

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
        TheaterId = showtime.TheaterId.ToString();
        MovieId = showtime.MovieId.ToString();
        RoomTypeId = showtime.RoomTypeId.ToString();
        StartAt = showtime.StartAt;
      }

      // Get all movies is active
      Movies = db.Movies.Where(m => m.Active == true);

      // Get all theaters
      Theaters = db.Theaters;

      // Get all room types
      RoomTypes = db.RoomTypes;
    }

    public void OnPost()
    {
      // edit showtime
      var showtime = db.ShowTimes.FirstOrDefault(t => t.Id.ToString() == Id);
      if (showtime != null)
      {
        showtime.TheaterId = Guid.Parse(TheaterId);
        showtime.MovieId = Guid.Parse(MovieId);
        showtime.RoomTypeId = Guid.Parse(RoomTypeId);
        showtime.StartAt = StartAt;

        db.SaveChanges();
      }

      // redirect to all show times pages
      Response.Redirect($"/Admin/Showtime");
    }
  }
}