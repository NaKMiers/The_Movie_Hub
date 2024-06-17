using Microsoft.AspNetCore.Mvc;

namespace the_movie_hub.Components
{
   public class HeaderViewComponent : ViewComponent
   {
      public IViewComponentResult Invoke()
      {
         return View("~/Components/Header/Header.cshtml");
      }
   }
}
