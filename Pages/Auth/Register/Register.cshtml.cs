using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Auth.Register
{
  public class RegisterModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties

    // Methods

    public async Task OnPostAsync(User user)
    {
      // check if email is unique
      var email = db.Users.FirstOrDefault(u => u.Email == user.Email);
      if (email != null)
      {
        ModelState.AddModelError("Email", "Email đã tồn tại");
        return;
      }

      var username = db.Users.FirstOrDefault(u => u.Username == user.Username);
      if (username != null)
      {
        ModelState.AddModelError("Username", "Username đã tồn tại");
        return;
      }

      var cccd = db.Users.FirstOrDefault(u => u.CCCD == user.CCCD);
      if (cccd != null)
      {
        ModelState.AddModelError("CCCD", "CCCD đã tồn tại");
        return;
      }

      // create user
      var newUser = new User
      {
        Id = Guid.NewGuid(),
        FullName = user.FullName,
        Username = user.Username,
        Email = user.Email,
        Phone = user.Phone,
        Password = user.Password,
        AuthType = user.AuthType,
        Birthday = user.Birthday,
        Expended = user.Expended,
        CCCD = user.CCCD,
      };

      db.Users.Add(newUser);
      await db.SaveChangesAsync();
    }
  }
}