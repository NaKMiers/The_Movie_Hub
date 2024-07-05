using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.RoomType
{
  public class AddRoomTypeModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;
    private readonly IWebHostEnvironment environment = environment;

    // Properties
    [BindProperty]
    public required string Title { get; set; }

    // Methods
    public void OnPost()
    {
      Console.WriteLine("Adding room type..." + Title);

      // add the room type
      var roomType = new Models.Main.RoomType
      {
        Id = Guid.NewGuid(),
        Title = Title
      };

      db.RoomTypes.Add(roomType);
      db.SaveChanges();

      // stay on the same page
      Response.Redirect("/Admin/RoomType/Add");
    }
  }
}