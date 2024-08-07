using Microsoft.AspNetCore.Mvc;
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
      [BindProperty]
      public required string Title { get; set; }

      [BindProperty]
      public required DateOnly ReleaseDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

      [BindProperty]
      public new required string Content { get; set; }

      [BindProperty]
      public required string Director { get; set; }

      [BindProperty]
      public required string Actors { get; set; }

      [BindProperty]
      public required int Duration { get; set; }

      [BindProperty]
      public required string TrailerUrl { get; set; }

      [BindProperty]
      public required string Country { get; set; }

      [BindProperty]
      public required string Note { get; set; }

      public IEnumerable<Models.Main.Genre> Genres { get; set; } = [];

      public void OnGet()
      {
         // get all genres
         Genres = [.. db.Genres];
      }

      // Methods
      public async Task OnPostAsync(List<string> SelectedGenres, IFormFile image, IFormFile banner)
      {
         Guid Id = Guid.NewGuid();

         var newMovie = new Models.Main.Movie
         {
            Id = Id,
            Title = Title.Trim(),
            ReleaseDate = ReleaseDate,
            Content = Content.Trim(),
            Director = Director.Trim(),
            Actors = Actors.Trim(),
            Duration = Duration,
            TrailerUrl = TrailerUrl,
            Country = Country.Trim(),
            Note = Note.Trim()
         };

         if (SelectedGenres != null)
         {
            foreach (var genreId in SelectedGenres)
            {
               db.MovieGenres.Add(new MovieGenre
               {
                  Id = Guid.NewGuid(),
                  MovieId = Id,
                  GenreId = Guid.Parse(genreId)
               });
            }
         }

         if (image != null && image.ContentType.Contains("image/"))
         {
            Console.WriteLine(image.FileName);

            string path = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(environment.WebRootPath, "uploads", path);

            var fileStream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            fileStream.Close();

            newMovie.Image = path;
         }

         if (banner != null && banner.ContentType.Contains("image/"))
         {
            Console.WriteLine(banner.FileName);

            string path = Guid.NewGuid().ToString() + Path.GetExtension(banner.FileName);
            var filePath = Path.Combine(environment.WebRootPath, "uploads", path);

            var fileStream = new FileStream(filePath, FileMode.Create);
            await banner.CopyToAsync(fileStream);

            fileStream.Close();

            newMovie.Banner = path;
         }

         db.Movies.Add(newMovie);
         await db.SaveChangesAsync();

         Response.Redirect("/Admin/Movie/Add");
         return;
      }
   }
}