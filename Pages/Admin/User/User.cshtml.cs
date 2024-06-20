using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.User
{
   public class UserModel(TheMovieHubDbContext db) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;

      // Properties
      [FromRoute]
      public IEnumerable<Models.Main.User> Users { get; set; } = [];

      // Methods
      public void OnGet()
      {
         Users = [.. db.Users];
      }
   }
}