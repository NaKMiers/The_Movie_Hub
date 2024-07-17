using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Auth.Register
{
  public class RegisterModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [BindProperty]
    public required string FullName { get; set; }

    [BindProperty]
    public required string Username { get; set; }

    [BindProperty]
    public required string Email { get; set; }

    [BindProperty]
    public required string Phone { get; set; }

    [BindProperty]
    public required string Password { get; set; }

    [BindProperty]
    public string? RePassword { get; set; }

    [BindProperty]
    public required DateOnly Birthday { get; set; } = new DateOnly(1900, 1, 1);

    [BindProperty]
    public required string CCCD { get; set; }

    // Methods
    public async Task<IActionResult> OnPostAsync()
    {
      // check if password and re-password are the same
      if (Password != RePassword)
      {
        ModelState.AddModelError("RePassword", "Mật khẩu không khớp");
        return Page();
      }

      // check if email is unique
      var email = db.Users.FirstOrDefault(u => u.Email == Email);
      if (email != null)
      {
        ModelState.AddModelError("Email", "Email đã tồn tại");
        return Page();
      }

      // check if username is unique
      var username = db.Users.FirstOrDefault(u => u.Username == Username);
      if (username != null)
      {
        ModelState.AddModelError("Username", "Username đã tồn tại");
        return Page();
      }

      // check if CCCD is unique
      var cccd = db.Users.FirstOrDefault(u => u.CCCD == CCCD);
      if (cccd != null)
      {
        ModelState.AddModelError("CCCD", "CCCD đã tồn tại");
        return Page();
      }

      // create user
      var newUser = new User
      {
        Id = Guid.NewGuid(),
        FullName = FullName,
        Username = Username,
        Email = Email,
        Phone = Phone,
        Password = Password,
        AuthType = "local",
        Birthday = Birthday,
        Expended = 0,
        CCCD = CCCD,
      };

      db.Users.Add(newUser);
      await db.SaveChangesAsync();

      HttpContext.Session.SetString("User", JsonConvert.SerializeObject(newUser));

      // Set success message
      TempData["SuccessMessage"] = "Đăng nhập thành công!";

      Response.Redirect("/");
      return Page();
    }
  }
}