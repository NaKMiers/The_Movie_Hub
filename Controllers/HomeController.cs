using the_movie_hub.Models;
using the_movie_hub.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace the_movie_hub.Controllers;

public class HomeController : Controller
{

    private readonly TheMovieHubDbContext DB;
    private readonly IHttpContextAccessor _httpContext;

    public HomeController(TheMovieHubDbContext databaseContext, IHttpContextAccessor httpContextAccessor)
    {
        DB = databaseContext;
        _httpContext = httpContextAccessor;
    }

    public IActionResult Index()
    {
        var movies = DB.Movies.ToList();

        return View();
    }

    public IActionResult PromotionProgram()
    {
        return View();
    }

    public IActionResult Event()
    {
        return View();
    }

    public IActionResult Entertaiment()
    {
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Restaurant()
    {
        return View();
    }

    public IActionResult Bowling()
    {
        return View();
    }

    public IActionResult Gym()
    {
        return View();
    }

    public IActionResult Coffee()
    {
        return View();
    }

    public IActionResult KidZone()
    {
        return View();
    }

    public IActionResult Billiard()
    {
        return View();
    }

    public IActionResult Opera()
    {
        return View();
    }

    public IActionResult PageNotFound()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
