using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Account.AccountProfile
{
  public class AccountProfileModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;
    private readonly IWebHostEnvironment environment = environment;

    // Properties
    [BindProperty]
    public IFormFile? Avatar { get; set; }

    [BindProperty]
    public required string FullName { get; set; }

    [BindProperty]
    public required string Email { get; set; }

    [BindProperty]
    public required string Phone { get; set; }

    [BindProperty]
    public DateOnly? Birthday { get; set; }

    [BindProperty]
    public required string OldPassword { get; set; }

    [BindProperty]
    public required string NewPassword { get; set; }

    [BindProperty]
    public required string ConfirmPassword { get; set; }

    // Methods
    public void OnGet()
    {
      // get userId from the session
      string? session = HttpContext.Session.GetString("User");
      var user = session != null ? JsonConvert.DeserializeObject<User>(session) : null;

      if (user == null)
      {
        // redirect to the login page
        Response.Redirect("/Login");
        return;
      }

      // Ensure user properties are not null before assigning
      FullName = user.FullName;
      Email = user.Email;
      Phone = user.Phone;
      Birthday = user.Birthday;
    }

    public async Task<IActionResult> OnPostChangeAvatarAsync()
    {
      // get userId from the session
      string? session = HttpContext.Session.GetString("User");
      var user = session != null ? JsonConvert.DeserializeObject<User>(session) : null;

      if (user == null)
      {
        // redirect to the login page
        Response.Redirect("/Login");
        return Page();
      }

      var updateUser = db.Users.FirstOrDefault(t => t.Id.ToString() == user.Id.ToString());

      if (updateUser == null)
      {
        Response.Redirect("/Account/Account-Profile");
        return Page();
      }

      // upload file, file must be an image
      if (Avatar != null && Avatar.ContentType.Contains("image/"))
      {
        string path = Guid.NewGuid().ToString() + Path.GetExtension(Avatar.FileName);
        var filePath = Path.Combine(environment.WebRootPath, "uploads", path);

        var fileStream = new FileStream(filePath, FileMode.Create);
        await Avatar.CopyToAsync(fileStream);

        fileStream.Close();

        // delete the old image
        if (!string.IsNullOrEmpty(updateUser.Avatar))
        {
          var oldFilePath = Path.Combine(environment.WebRootPath, "uploads", updateUser.Avatar);
          if (System.IO.File.Exists(oldFilePath))
          {
            System.IO.File.Delete(oldFilePath);
          }
        }

        Console.WriteLine(path);

        updateUser.Avatar = path;
      }

      // update the user by id
      await db.SaveChangesAsync();

      // update the session 
      HttpContext.Session.SetString("User", JsonConvert.SerializeObject(updateUser));

      // Set success message
      TempData["SuccessMessage"] = "Đổi ảnh đại diện thành công!";
      FullName = user.FullName;
      Email = user.Email;
      Phone = user.Phone;
      Birthday = user.Birthday;


      // redirect to the user page
      return Page();
    }

    public async Task<IActionResult> OnPostUpdateInfo()
    {
      Console.WriteLine(12312);

      string? session = HttpContext.Session.GetString("User");
      var user = session != null ? JsonConvert.DeserializeObject<User>(session) : null;

      if (user == null)
      {
        // redirect to the login page
        Response.Redirect("/Login");
        return Page();
      }

      var updateUser = db.Users.FirstOrDefault(t => t.Id.ToString() == user.Id.ToString());

      if (updateUser == null)
      {
        return Page();
      }


      // Ensure user properties are not null before assigning
      updateUser.FullName = FullName;
      updateUser.Email = Email;
      updateUser.Phone = Phone;
      updateUser.Birthday = Birthday;

      // update the user by id
      await db.SaveChangesAsync();

      // update the session
      HttpContext.Session.SetString("User", JsonConvert.SerializeObject(updateUser));

      // Set success message
      TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";

      return Page();
    }

    public async Task<IActionResult> OnPostChangePassword()
    {
      // get userId from the session
      string? session = HttpContext.Session.GetString("User");
      var user = session != null ? JsonConvert.DeserializeObject<User>(session) : null;

      if (user == null)
      {
        // redirect to the login page
        Response.Redirect("/Login");
        return Page();
      }

      // check if the new password and confirm password are the same
      if (NewPassword != ConfirmPassword)
      {
        // Ensure user properties are not null before assigning
        FullName = user.FullName;
        Email = user.Email;
        Phone = user.Phone;
        Birthday = user.Birthday;

        ModelState.AddModelError("Password", "Mật khẩu không khớp");
        return Page();
      }





      var updateUser = db.Users.FirstOrDefault(t => t.Id.ToString() == user.Id.ToString());
      if (updateUser == null)
      {
        return Page();
      }

      // check if the old password is correct
      if (updateUser.Password != OldPassword)
      {
        // Ensure user properties are not null before assigning
        FullName = user.FullName;
        Email = user.Email;
        Phone = user.Phone;
        Birthday = user.Birthday;

        ModelState.AddModelError("Password", "Mật khẩu cũ không đúng");
        return Page();
      }

      // check if the new password is the same as the old password
      if (updateUser.Password == NewPassword)
      {
        // Ensure user properties are not null before assigning
        FullName = user.FullName;
        Email = user.Email;
        Phone = user.Phone;
        Birthday = user.Birthday;

        ModelState.AddModelError("Password", "Mật khẩu mới không được trùng với mật khẩu cũ");
        return Page();
      }

      // update the password
      updateUser.Password = NewPassword;
      await db.SaveChangesAsync();

      // Ensure user properties are not null before assigning
      FullName = user.FullName;
      Email = user.Email;
      Phone = user.Phone;
      Birthday = user.Birthday;

      // update the session
      HttpContext.Session.SetString("User", JsonConvert.SerializeObject(updateUser));

      // Set success message
      TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
      return Page();
    }
  }
}