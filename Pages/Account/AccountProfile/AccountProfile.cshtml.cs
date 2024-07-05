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

    // Methods
    public async Task OnPostChangeAvatarAsync()
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

      var updateUser = db.Users.FirstOrDefault(t => t.Id.ToString() == user.Id.ToString());
      if (updateUser == null)
      {
        Response.Redirect("/Account/Account-Profile");
        return;
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

        updateUser.Avatar = path;
      }

      // update the user by id
      await db.SaveChangesAsync();

      // redirect to the user page
      Response.Redirect("/Account/Account-Profile");
      return;
    }
  }
}