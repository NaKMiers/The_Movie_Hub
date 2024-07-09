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

  public class RoomTypeViewModel
  {
    public string Title { get; set; }
    public List<ShowtimeViewModel> ShowTimes { get; set; }
    public string Id { get; set; }
    public string RoomId { get; set; }
  }

  public class TheaterViewModel
  {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public List<RoomTypeViewModel> RoomTypeShowTimes { get; internal set; } = [];
  }

  public class ApiModel(TheMovieHubDbContext db) : PageModel
  {
    private readonly TheMovieHubDbContext db = db;

    // Get Show Times Theaters
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
          .Include(s => s.Room)
          .ThenInclude(r => r.RoomType)
          .ToList();

      var theaters = showTimes
               .GroupBy(s => s.Theater)
               .Select(g => new TheaterViewModel
               {
                 Id = g.Key.Id.ToString(),
                 Name = g.Key.Name,
                 Address = g.Key.Address,
                 RoomTypeShowTimes = g.GroupBy(r => r.Room.RoomType)
                       .Select(rt => new RoomTypeViewModel
                       {
                         Id = rt.Key.Id.ToString(),
                         Title = rt.Key.Title,
                         RoomId = rt.First().Room.Id.ToString(), // Thêm RoomId vào đây
                         ShowTimes = rt.Select(s => new ShowtimeViewModel
                         {
                           Id = s.Id.ToString(),
                           Time = s.StartAt.ToString("HH:mm")
                         }).ToList()
                       }).ToList()
               }).ToList();

      return new JsonResult(new { theaters, message = "Successfully" });
    }

    // Get Ticket Types
    public IActionResult OnGetTicketTypes(string RoomId)
    {
      // get room type by room id
      var room = db.Rooms
          .Where(r => r.Id.ToString() == RoomId)
          .Include(r => r.RoomType)
          .FirstOrDefault();

      if (room == null)
      {
        return new JsonResult(new { message = "Room not found" });
      }

      var ticketTypes = db.TicketTypes
          .Where(t => t.RoomTypeId.ToString() == room.RoomType.Id.ToString())
        .ToList();

      // get all seats of room
      var seats = db.Seats
          .Where(s => s.RoomId.ToString() == RoomId);

      // return response
      return new JsonResult(new { ticketTypes, seats, room, message = "Successfully" });
    }
  }
}
