using Microsoft.AspNetCore.Mvc;

namespace the_movie_hub.Components
{
   public class Footer : ViewComponent
   {
      public IViewComponentResult Invoke()
      {
         return View("~/Components/Footer/Footer.cshtml");
      }
   }
}
