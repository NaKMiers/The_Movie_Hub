using Microsoft.AspNetCore.Mvc;

namespace the_movie_hub.Components
{
  public class AdminMenuViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke()
    {
      return View("~/Components/AdminMenu/AdminMenu.cshtml");
    }
  }
}
