using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Movie
{
   public class EditMovieModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
   {
      // Database context
      private readonly TheMovieHubDbContext db = db;
      private readonly IWebHostEnvironment environment = environment;

      // Properties
      public IEnumerable<Models.Main.Genre> Genres { get; set; } = [];
      public List<string> MovieGenres { get; set; } = [];

      [FromRoute]
      public required string Id { get; set; }
      public Models.Main.Movie? Movie { get; set; }

      public void OnGet()
      {
         // get movie
         Movie = db.Movies.FirstOrDefault(m => m.Id.ToString() == Id);

         if (Movie == null)
         {
            Response.Redirect("/Admin/Movie");
            return;
         }

         // get all genres
         Genres = db.Genres;

         // get movie-genres
         MovieGenres = [.. db.MovieGenres.Where(m => m.MovieId == Movie.Id).Select(m => m.GenreId.ToString().ToLower())];
      }

      // Methods
      public async Task OnPostAsync(Models.Main.Movie movie, List<string> SelectedGenres, IFormFile image, IFormFile banner)
      {
         var MovieUpdate = db.Movies.FirstOrDefault(t => t.Id.ToString() == Id);
         if (MovieUpdate == null)
         {
            // redirect to the theaters page
            Response.Redirect("/Admin/Movie");
            return;
         }

         // update the movie
         MovieUpdate.Title = movie.Title.Trim();
         MovieUpdate.ReleaseDate = movie.ReleaseDate;
         MovieUpdate.Content = movie.Content.Trim();
         MovieUpdate.Director = movie.Director.Trim();
         MovieUpdate.Actors = movie.Actors.Trim();
         MovieUpdate.Duration = movie.Duration;
         MovieUpdate.TrailerUrl = movie.TrailerUrl;
         MovieUpdate.Country = movie.Country;
         MovieUpdate.Note = movie.Note;

         // update the movie-genres
         if (SelectedGenres != null)
         {
            var movieGenres = db.MovieGenres.Where(m => m.MovieId == MovieUpdate.Id);

            foreach (var movieGenre in movieGenres)
            {
               db.MovieGenres.Remove(movieGenre);
            }

            Console.WriteLine("SelectedGenres.Count: " + SelectedGenres.Count);

            foreach (var genreId in SelectedGenres)
            {
               db.MovieGenres.Add(new MovieGenre
               {
                  Id = Guid.NewGuid(),
                  MovieId = MovieUpdate.Id,
                  GenreId = Guid.Parse(genreId)
               });
            }
         }


         // update movie banner
         if (banner != null && banner.ContentType.Contains("image/"))
         {
            Console.WriteLine(banner.FileName);

            string path = Guid.NewGuid().ToString() + Path.GetExtension(banner.FileName);
            var filePath = Path.Combine(environment.WebRootPath, "uploads", path);

            var fileStream = new FileStream(filePath, FileMode.Create);
            await banner.CopyToAsync(fileStream);

            fileStream.Close();

            // delete the old banner
            if (!string.IsNullOrEmpty(MovieUpdate.Banner))
            {
               var oldFilePath = Path.Combine(environment.WebRootPath, "uploads", MovieUpdate.Banner);
               if (System.IO.File.Exists(oldFilePath))
               {
                  System.IO.File.Delete(oldFilePath);
               }
            }

            MovieUpdate.Banner = path;

         }

         // update movie image
         if (image != null && image.ContentType.Contains("image/"))
         {
            Console.WriteLine(image.FileName);

            string path = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(environment.WebRootPath, "uploads", path);

            var fileStream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            fileStream.Close();

            // delete the old image
            if (!string.IsNullOrEmpty(MovieUpdate.Image))
            {
               var oldFilePath = Path.Combine(environment.WebRootPath, "uploads", MovieUpdate.Image);
               if (System.IO.File.Exists(oldFilePath))
               {
                  System.IO.File.Delete(oldFilePath);
               }
            }

            MovieUpdate.Image = path;

         }

         // update database
         await db.SaveChangesAsync();

         // redirect to the theaters page
         Response.Redirect("/Admin/Movie");
         return;
      }
   }
}