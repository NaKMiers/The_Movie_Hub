using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
         Guid Id = Guid.NewGuid();
         Theater = new Models.Main.Theater
         {
            Id = Id,
            Name = theater.Name,
            Address = theater.Address,
            City = theater.City,
         };

         if (image == null || !image.ContentType.Contains("image/"))
         {
            ModelState.AddModelError("Image", "The file must be an image.");
            return;
         }

         // upload file, file must be an image
         string path = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
         var filePath = Path.Combine(environment.WebRootPath, "uploads", path);

         var fileStream = new FileStream(filePath, FileMode.Create);
         await image.CopyToAsync(fileStream);
         fileStream.Close();

         Theater.Image = path;

         // add the theater to the database
         db.Theaters.Add(Theater);

         await db.SaveChangesAsync();
      }
   }
}