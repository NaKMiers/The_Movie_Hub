using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Room
{
  public class AddRoomModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [FromRoute]
    public required string TheaterId { get; set; }

    [BindProperty]
    public required string Name { get; set; }

    [BindProperty]
    public required string RoomTypeId { get; set; }

    public Models.Main.Theater? Theater { get; set; }

    public IEnumerable<Models.Main.RoomType> RoomTypes { get; set; } = [];

    // Methods
    public void OnGet()
    {
      // get theater
      Theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == TheaterId);

      // check if theater is null
      if (Theater == null)
      {
        // redirect to the theaters page
        Response.Redirect($"/Admin/Room/{TheaterId}");
        return;
      }

      // get room types
      RoomTypes = db.RoomTypes;
    }

    public void OnPost()
    {
      // create new room
      var room = new Models.Main.Room
      {
        Id = Guid.NewGuid(),
        Name = Name,
        RoomTypeId = Guid.Parse(RoomTypeId),
        TheaterId = Guid.Parse(TheaterId)
      };

      // increase the theater's room count
      var theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == TheaterId);
      if (theater != null)
      {
        theater.RoomAmount = theater.RoomAmount == null ? 1 : theater.RoomAmount + 1;
      }

      db.Rooms.Add(room);
      db.SaveChanges();

      // redirect to the same page
      Response.Redirect($"/Admin/Room/{TheaterId}/Add");
    }
  }
}