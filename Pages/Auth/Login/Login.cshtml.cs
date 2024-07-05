using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace the_movie_hub.Pages.Auth.Login
{
  public class LoginModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [BindProperty]
    public required string Email { get; set; }

    [BindProperty]
    public required string Password { get; set; }

    // Methods

    public IActionResult OnPost()
    {
      // get user to check
      var user = db.Users.FirstOrDefault(u => u.Email.Trim() == Email.Trim());

      // check if user exists or not
      if (user == null)
      {
        ModelState.AddModelError("User", "Người dùng không tồn tại");
        return Page();
      }

      if (user.Password.Trim() != Password.Trim())
      {
        ModelState.AddModelError("Password", "Sai mật khẩu");

        // return page with error and data
        return Page();
      }

      HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

      // return to home page
      Response.Redirect("/");
      return Page();
    }
  }
}