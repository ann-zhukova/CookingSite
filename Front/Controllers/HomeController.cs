using System.Diagnostics;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Front.Models;

namespace Front.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserRepository _userRepository;
    public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [Route("/")]
    [HttpGet]
    public async Task<IActionResult> Index(string url)
    {
        var res = await _userRepository.GetUsersAsync();
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}