using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Areas.ProjectManagement.Controllers;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogTrace("Accessed Home Index at {time}", DateTime.Now);
        return View();
    }

    public IActionResult Privacy()
    {
        _logger.LogTrace("Accessed Privacy at {time}", DateTime.Now);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("Error on Home Index at {time}", DateTime.Now);
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpGet]
    public IActionResult GeneralSearch(string searchType, string searchString)
    {
        _logger.LogInformation("Accessed Search at {time}", DateTime.Now);

        searchType = searchType?.Trim().ToLower();
        if (string.IsNullOrWhiteSpace(searchType)||string.IsNullOrWhiteSpace(searchString))
        {
            return RedirectToAction(nameof(Index));
        }
        if (searchType == "projects")
        {
            _logger.LogInformation("Project Search at {time}", DateTime.Now);
            return RedirectToAction(nameof(ProjectController.Search), "Project", new { area = "ProjectManagement" , searchString });

        }

        if (searchType == "tasks")
        {
            _logger.LogInformation("Task Search at {time}", DateTime.Now);
            return RedirectToAction(nameof(ProjectTaskController.Search), "ProjectTask", new { area = "ProjectManagement", searchString });
        }

        return RedirectToAction(nameof(Index), "Home");

    }
}
