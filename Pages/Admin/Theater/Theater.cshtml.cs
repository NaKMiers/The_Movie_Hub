using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Theater
{
   public class TheaterModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;
      private readonly IWebHostEnvironment environment = environment;

      // Properties
      public IEnumerable<Models.Main.Theater> Theaters { get; set; } = [];

      public Models.Main.Theater Theater { get; set; }

      // Methods
      public void OnGet()
      {
         Theaters = [.. db.Theaters];
      }

      public void OnPostDelete(string Id)
      {
         var theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == Id);
         if (theater != null)
         {
            db.Theaters.Remove(theater);
            db.SaveChanges();
         }

         // redirect to the theaters page
         Response.Redirect("/Admin/Theater");
      }
   }
}