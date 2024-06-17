using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using the_movie_hub.Models;
using the_movie_hub.Models.Main;
namespace the_movie_hub.Controllers;

public class AuthController : Controller
{
    // declare db context
    private readonly TheMovieHubDbContext _db;
    private readonly IHttpContextAccessor _httpContext;

    public AuthController(TheMovieHubDbContext databaseContext, IHttpContextAccessor httpContextAccessor)
    {
        _db = databaseContext;
        _httpContext = httpContextAccessor;

    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost] // login
    public IActionResult Login(User data)
    {
        Console.WriteLine(data.ToString());

        // get user from database
        User user = _db.Users.FirstOrDefault(u => (u.Email == data.Email || u.Username == data.Email || u.Phone == data.Phone) && u.Password == data.Password);

        // user doesn't exist
        if (user == null)
        {
            ModelState.AddModelError("Email", "Email or password is incorrect");
            return View(user);
        }

        // set user to session
        _httpContext?.HttpContext?.Session.SetString("Id", user.Id.ToString());
        _httpContext?.HttpContext?.Session.SetString("Email", user.Email);
        _httpContext?.HttpContext?.Session.SetString("FullName", user.FullName);
        _httpContext?.HttpContext?.Session.SetString("Phone", user.Phone);
        _httpContext?.HttpContext?.Session.SetString("AuthType", user.AuthType);
        _httpContext?.HttpContext?.Session.SetString("Birthday", user.Birthday?.ToString() ?? "");
        _httpContext?.HttpContext?.Session.SetString("Expended", user.Expended.ToString());

        // return View(user);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View("~/Views/Auth/Login.cshtml");
    }

    [HttpPost] // register
    public IActionResult Register(User data)
    {
        // check if email has already existed
        var user = _db.Users.FirstOrDefault(u => u.Email == data.Email || u.Username == data.Email || u.Phone == data.Phone);

        // user already exists
        if (user != null)
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View("~/Views/Auth/Login.cshtml", data);
        }

        // add user to database
        _db.Users.Add(data);
        _db.SaveChanges();

        // set user to session
        // HttpContext.Session.SetString("UserId", data.Id.ToString());
        // HttpContext.Session.SetString("UserEmail", data.Email);
        // HttpContext.Session.SetString("UserFullName", data.FullName);
        // HttpContext.Session.SetString("UserPhone", data.Phone);
        // HttpContext.Session.SetString("AuthType", data.AuthType);
        // HttpContext.Session.SetString("UserBirthday", data.Birthday?.ToString() ?? "");
        // HttpContext.Session.SetString("UserExpended", data.Expended.ToString());

        Console.WriteLine(data.FullName);
        // return register page with user data
        return View("~/Views/Auth/Login.cshtml", data);
    }


    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
