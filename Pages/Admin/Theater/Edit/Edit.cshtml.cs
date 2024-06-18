using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Theater
{
   public class EditTheaterModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;
      private readonly IWebHostEnvironment environment = environment;

      // Properties
      [FromRoute]
      public required string Id { get; set; }
      public Models.Main.Theater? Theater { get; set; }

      public void OnGet()
      {
         // get theater by id
         Theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == Id);

         if (Theater == null)
         {
            // redirect to the theaters page
            Response.Redirect("/Admin/Theater/");
            return;
         }
      }

      // Methods
      public async Task OnPostAsync(Models.Main.Theater theater, IFormFile image)
      {
         var Theater = db.Theaters.FirstOrDefault(t => t.Id.ToString() == Id);
         if (Theater == null)
         {
            // redirect to the theaters page
            Response.Redirect("/Admin/Theater");
            return;
         }

         // create a new theater
         Theater.Name = theater.Name;
         Theater.Address = theater.Address;
         Theater.RoomAmount = theater.RoomAmount;

         // upload file, file must be an image
         if (image != null && image.ContentType.Contains("image/"))
         {
            string path = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(environment.WebRootPath, "uploads", path);

            var fileStream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            fileStream.Close();

            // delete the old image
            if (!string.IsNullOrEmpty(Theater.Image))
            {
               var oldFilePath = Path.Combine(environment.WebRootPath, "uploads", Theater.Image);
               if (System.IO.File.Exists(oldFilePath))
               {
                  System.IO.File.Delete(oldFilePath);
               }
            }

            Theater.Image = path;
         }

         // update the theater by id
         await db.SaveChangesAsync();

         // redirect to the theaters page
         Response.Redirect("/Admin/Theater/");
         return;
      }
   }
}