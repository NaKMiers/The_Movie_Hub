using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using the_movie_hub.Models;

namespace the_movie_hub.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
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
