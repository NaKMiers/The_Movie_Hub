using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using the_movie_hub.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace the_movie_hub.Pages.Api
{
  public class ShowtimeViewModel
  {
    public DateTime Date { get; set; }
    public List<TheaterViewModel> Theaters { get; set; } = [];
    public string Id { get; internal set; }
    public string Time { get; internal set; }
  }

  internal class RoomTypeViewModel
  {
    public string Title { get; set; }
    public object ShowTimes { get; set; }
  }

  public class TheaterViewModel
  {
    public required String Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public object RoomTypeShowTimes { get; internal set; }
  }

  public class ApiModel(TheMovieHubDbContext db) : PageModel
  {
    private readonly TheMovieHubDbContext db = db;

    public IActionResult OnGetShowTimesTheaters(string Date, string MovieId, string City)
    {
      Console.WriteLine("Date: " + Date);
      Console.WriteLine(MovieId);
      Console.WriteLine(City);

      // get show times by date
      var selectedDate = DateTime.ParseExact(Date, "dd/MM", CultureInfo.InvariantCulture);

      // group showTimes
      var showTimes = db.ShowTimes
          .Where(s => s.MovieId.ToString() == MovieId && s.StartAt.Date == selectedDate.Date && s.Theater.City == City)
          .OrderBy(s => s.StartAt)
          .Include(s => s.Theater)
          .Include(s => s.RoomType)
          .ToList();

      var theaters = showTimes
    .GroupBy(s => s.Theater)
    .Select(g => new TheaterViewModel
    {
      Id = g.Key.Id.ToString(),
      Name = g.Key.Name,
      Address = g.Key.Address,
      RoomTypeShowTimes = g.GroupBy(r => r.RoomType.Title)
                            .Select(rt => new RoomTypeViewModel
                            {
                              Title = rt.Key,
                              ShowTimes = rt.Select(s => new ShowtimeViewModel
                              {
                                Id = s.Id.ToString(),
                                Time = s.StartAt.ToString("HH:mm")
                              }).ToList()
                            }).ToList()
    }).ToList();

      return new JsonResult(new { theaters, message = "Successfully" });
    }
  }


}
