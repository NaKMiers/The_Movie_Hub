using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.RoomType
{
  public class RoomTypeModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    public IEnumerable<Models.Main.RoomType> RoomTypes { get; set; } = [];


    // Methods
    public void OnGet()
    {
      RoomTypes = db.RoomTypes;
    }

    public void OnPostDelete(string Id)
    {
      var roomType = db.RoomTypes.FirstOrDefault(t => t.Id.ToString() == Id);
      if (roomType != null)
      {
        db.RoomTypes.Remove(roomType);
        db.SaveChanges();
      }

      // redirect to the same page
      Response.Redirect("/Admin/RoomType");
    }
  }
}