using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Showtime
{
  public class AddShowtimeModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;
    private readonly IWebHostEnvironment environment = environment;

    // Properties
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
      // Get all movies is active
      Movies = db.Movies.Where(m => m.Active == true);

      // Get all theaters
      Theaters = db.Theaters;

      // Get all room types
      RoomTypes = db.RoomTypes;
    }

    public void OnPost()
    {
      Console.WriteLine("Adding showtime..." + StartAt);

      // add the showtime
      var showtime = new Models.Main.Showtime
      {
        Id = Guid.NewGuid(),
        TheaterId = Guid.Parse(TheaterId),
        MovieId = Guid.Parse(MovieId),
        RoomTypeId = Guid.Parse(RoomTypeId),
        StartAt = StartAt
      };

      db.ShowTimes.Add(showtime);
      db.SaveChanges();

      // stay on the same page
      Response.Redirect("/Admin/Showtime/Add");
    }
  }
}