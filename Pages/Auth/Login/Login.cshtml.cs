using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;
using Microsoft.AspNetCore.Http; // Add this
using Newtonsoft.Json; // Add this

namespace the_movie_hub.Pages.Auth.Login
{
  public class LoginModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties

    // Methods

    public async Task OnPostAsync(string Email, string Password, bool Remember)
    {
      Console.WriteLine(Email);
      Console.WriteLine(Password);
      Console.WriteLine(Remember);

      // get user to check
      var user = db.Users.FirstOrDefault(u => u.Email.Trim() == Email.Trim());

      // check if user exists or not
      if (user == null)
      {
        ModelState.AddModelError("User", "Người dùng không đã tồn tại");
        return;
      }

      if (user.Password.Trim() != Password.Trim())
      {
        ModelState.AddModelError("Password", "Sai mật khẩu");
        return;
      }

      HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

      Response.Redirect("/");
      return;
    }
  }
}