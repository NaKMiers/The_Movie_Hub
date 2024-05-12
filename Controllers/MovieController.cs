using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using the_movie_hub.Models;

namespace the_movie_hub.Controllers;

public class MovieController : Controller
{
    private readonly ILogger<MovieController> _logger;

    public MovieController(ILogger<MovieController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult MovieDetail()
    {
        return View();
    }

    public IActionResult Search()
    {
        return View();
    }

    public IActionResult MovieShowing()
    {
        return View();
    }

    public IActionResult Upcoming()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
