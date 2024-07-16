using Microsoft.AspNetCore.Mvc;
using the_movie_hub.Models.Main;
using the_movie_hub.Models.ViewModels;

namespace the_movie_hub.Components.Slider
{
  public class ShowingSliderViewComponent(TheMovieHubDbContext db) : ViewComponent
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Methods
    public IViewComponentResult Invoke(IEnumerable<Movie> Movies)
    {
      return View("~/Components/ShowingSlider/ShowingSlider.cshtml", new SliderViewModel { Movies = Movies });
    }
  }
}
