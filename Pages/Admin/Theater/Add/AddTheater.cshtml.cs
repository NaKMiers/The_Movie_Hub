using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Theater
{
   public class AddTheaterModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;
      private readonly IWebHostEnvironment environment = environment;

      // Properties
      public Models.Main.Theater? Theater { get; set; }

      // Methods
      public async Task OnPostAsync(Models.Main.Theater theater, IFormFile image)
      {
         // create a new theater
         Theater = new Models.Main.Theater
         {
            Id = Guid.NewGuid(),
            Name = theater.Name,
            Address = theater.Address,
            RoomAmount = theater.RoomAmount
         };

         // upload file, file must be an image
         if (image != null && image.ContentType.Contains("image/"))
         {
            string path = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(environment.WebRootPath, "uploads", path);

            var fileStream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            Theater.Image = path;

            // add the theater to the database
            db.Theaters.Add(Theater);
            await db.SaveChangesAsync();

            // redirect to the theaters page
            Response.Redirect("/Admin/Theater");
         }
         else
         {
            ModelState.AddModelError("Image", "The file must be an image.");
         }
      }
   }
}