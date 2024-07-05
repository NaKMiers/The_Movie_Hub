using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.RoomType
{
  public class EditRoomTypeModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;
    private readonly IWebHostEnvironment environment = environment;

    // Properties
    public Models.Main.RoomType? RoomType { get; set; }

    [FromRoute]
    public required string Id { get; set; }

    [BindProperty]
    public required string Title { get; set; }

    // Methods
    public void OnGet()
    {
      RoomType = db.RoomTypes.FirstOrDefault(t => t.Id.ToString() == Id);

      if (RoomType == null)
      {
        Response.Redirect("/Admin/RoomType");
        return;
      }

      Title = RoomType.Title;
    }

    public void OnPost()
    {
      var updateRoomType = db.RoomTypes.FirstOrDefault(g => g.Id.ToString() == Id);

      if (updateRoomType == null)
      {
        Response.Redirect("/Admin/RoomType");
        return;
      }

      updateRoomType.Title = Title;
      db.SaveChanges();

      // redirect to the all room types page
      Response.Redirect("/Admin/RoomType");
      return;
    }
  }
}