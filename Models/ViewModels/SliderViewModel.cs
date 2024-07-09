using the_movie_hub.Models.Main;

namespace the_movie_hub.Models.ViewModels
{
  public class SliderViewModel
  {
    public required IEnumerable<Movie> Movies { get; set; }
  }
}