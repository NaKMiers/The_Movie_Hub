using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace the_movie_hub.Pages.Auth.Logout
{
  public class LogoutModel() : PageModel
  {
    public IActionResult OnGet()
    {
      // Clear the session
      HttpContext.Session.Clear();

      Console.WriteLine(21312321);

      // Redirect to the login page or home page
      Response.Redirect("/login");

      return Page();
    }
  }
}