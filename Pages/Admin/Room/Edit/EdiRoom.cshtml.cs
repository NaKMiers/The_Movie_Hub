using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Room
{
  public class EditRoomModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [FromRoute]
    public required string Id { get; set; }

    [FromRoute]
    public required string TheaterId { get; set; }

    [BindProperty]
    public required string Name { get; set; }

    [BindProperty]
    public required int Capacity { get; set; }

    [BindProperty]
    public required string RoomTypeId { get; set; }

    public Models.Main.Theater? Theater { get; set; }

    public IEnumerable<Models.Main.RoomType> RoomTypes { get; set; } = [];

    // Methods
    public void OnGet()
    {
      // get room
      var room = db.Rooms.FirstOrDefault(t => t.Id.ToString() == Id);

      // check if room is null
      if (room == null)
      {
        // redirect to the rooms page
        Response.Redirect($"/Admin/Room/{TheaterId}");
        return;
      }

      // set room properties
      Name = room.Name;
      RoomTypeId = room.RoomTypeId.ToString();

      // get theater
      Theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == TheaterId);

      // get room types
      RoomTypes = db.RoomTypes;

      // show room types
      foreach (var roomType in RoomTypes)
      {
        Console.WriteLine(roomType.Id + " " + roomType.Title);
      }
    }

    public void OnPost()
    {
      Console.WriteLine(RoomTypeId);

      // get room
      var room = db.Rooms.FirstOrDefault(t => t.Id.ToString() == Id);

      // check if room is null
      if (room == null)
      {
        // redirect to the rooms page
        Response.Redirect($"/Admin/Room/{TheaterId}");
        return;
      }

      // update room
      room.Name = Name;
      room.RoomTypeId = Guid.Parse(RoomTypeId);

      db.SaveChanges();

      // redirect to the same page
      Response.Redirect($"/Admin/Room/{TheaterId}");
    }
  }
}