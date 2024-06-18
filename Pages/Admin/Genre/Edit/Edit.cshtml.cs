using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Genre
{
   public class EditGenreModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;

      // Properties
      [FromRoute]
      public required string Id { get; set; }
      public Models.Main.Genre? Genre { get; set; }

      public void OnGet()
      {
         Genre = db.Genres.FirstOrDefault(g => g.Id.ToString() == Id);

         if (Genre == null)
         {
            Response.Redirect("/Admin/Genre");
         }
      }

      // Methods
      public async Task OnPostAsync(Models.Main.Genre genre)
      {
         var updateGenre = db.Genres.FirstOrDefault(g => g.Id.ToString() == Id);

         if (updateGenre == null)
         {
            Response.Redirect("/Admin/Genre");
            return;
         }

         updateGenre.Title = genre.Title;
         await db.SaveChangesAsync();

         // redirect to the theaters page
         Response.Redirect("/Admin/Genre");
         return;
      }
   }
}