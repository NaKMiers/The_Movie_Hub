using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Home.Search
{
  public class SearchModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public IEnumerable<Theater> Theaters { get; set; } = [];

    // Methods
    public void OnGet()
    {

      if (!string.IsNullOrEmpty(Search))
      {
        Console.WriteLine("Search: " + Search);
        // Search for theaters based on the query parameter
        Theaters = db.Theaters
            .Where(t => t.Name.ToLower().Contains(Search.ToLower()) || t.Address.ToLower().Contains(Search.ToLower()));
      }
      else
      {
        // If no query parameter is provided, return all theaters
        Theaters = db.Theaters;
      }
    }
  }
}