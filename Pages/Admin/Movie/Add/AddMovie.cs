using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Movie
{
   public class AddMovieModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;
      private readonly IWebHostEnvironment environment = environment;

      // Properties
      public Models.Main.Movie? Movie { get; set; }
      public IEnumerable<MovieGenre>? Genres { get; set; }

      public void OnGet()
      {
         // get all genres
         Genres = [.. db.MovieGenres];
      }

      // Methods
      public async Task OnPostAsync(Models.Main.Movie movie, IFormFile image)
      {
         // create a new movie
         // Movie = new Models.Main.Movie
         // {
         //    Id = Guid.NewGuid(),
         //    Title = movie.Title,
         //    ReleaseDate = movie.ReleaseDate,
         //    Content = movie.Content,
         //    Director = movie.Director,
         //    Actors = movie.Actors,
         //    Duration = movie.Duration,
         //    TrailerUrl = movie.TrailerUrl,
         //    Rating = movie.Rating,
         //    Country = movie.Country,
         //    Note = movie.Note,
         //    Active = movie.Active,
         //    // MovieGenres
         //    // Showtimes
         // };

         // // upload file, file must be an image
         // if (image != null && image.ContentType.Contains("image/"))
         // {
         //    string path = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
         //    var filePath = Path.Combine(environment.WebRootPath, "uploads", path);

         //    var fileStream = new FileStream(filePath, FileMode.Create);
         //    await image.CopyToAsync(fileStream);

         //    Movie.Image = path;

         //    // add the movie to the database
         //    db.Movies.Add(Movie);
         //    await db.SaveChangesAsync();

         //    // redirect to the movies page
         //    Response.Redirect("/Admin/Movie");
         // }
         // else
         // {
         //    ModelState.AddModelError("Image", "The file must be an image.");
         // }
      }
   }
}