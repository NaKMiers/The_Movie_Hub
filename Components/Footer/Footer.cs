using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Components
{
   public class FooterViewComponent(TheMovieHubDbContext db) : ViewComponent
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;

      // Properties


      // Methods
      public async Task<IViewComponentResult> InvokeAsync()
      {
         var theaters = await db.Theaters.ToListAsync();
         return View("~/Components/Footer/Footer.cshtml", theaters);
      }
   }
}
