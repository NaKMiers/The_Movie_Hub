using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using the_movie_hub.Models;

namespace the_movie_hub.Controllers;

public class ManagementController : Controller
{
    private readonly ILogger<ManagementController> _logger;

    public ManagementController(ILogger<ManagementController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Export()
    {
        return View();
    }
    public IActionResult Cinema()
    {
        return View();
    }
    public IActionResult Customer()
    {
        return View();
    }
    public IActionResult Ticket()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
