using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Ticket.Index
{
  public class IndexModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    public IEnumerable<Theater> Theaters { get; set; } = [];

    // Methods
    public void OnGet()
    {
      // get all theaters
      Theaters = db.Theaters;
    }
  }
}