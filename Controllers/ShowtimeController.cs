using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using the_movie_hub.Models;

namespace the_movie_hub.Controllers;

public class ShowtimeController : Controller
{
    private readonly ILogger<ShowtimeController> _logger;

    public ShowtimeController(ILogger<ShowtimeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
