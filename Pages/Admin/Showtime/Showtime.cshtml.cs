using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Showtime
{
  public class ShowtimeModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    public IEnumerable<Models.Main.Showtime> ShowTimes { get; set; } = [];

    // Methods
    public void OnGet()
    {
      // get all movie and include the genres, group by theater but still show all and room type
      ShowTimes = db.ShowTimes.Include(t => t.Movie).Include(t => t.Theater).Include(t => t.Room).ThenInclude(r => r.RoomType).OrderBy(t => t.Theater);
    }

    public void OnPostDelete(string Id)
    {
      var showtime = db.ShowTimes.FirstOrDefault(t => t.Id.ToString() == Id);
      if (showtime != null)
      {
        db.ShowTimes.Remove(showtime);
        db.SaveChanges();
      }

      // redirect to the same page
      Response.Redirect("/Admin/Showtime");
    }
  }
}