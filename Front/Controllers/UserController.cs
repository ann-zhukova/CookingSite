using Front.Helpers.UserHelper;
using Front.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers;

[Route("users")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserHelper _userHelper;

    public UserController(ILogger<UserController> logger, IUserHelper userHelper)
    {
        _logger = logger;
        _userHelper = userHelper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("login")]
    public IActionResult GetLogin()
    {
        return View("Index");
    }

    [HttpGet]
    [Route("register")]
    public IActionResult GetRegister()
    {
        return View("Index");
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestJs request)
    {
        _logger.Log(LogLevel.Information, request.ToString());
        return await _userHelper.RegisterUser(request);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestJs request)
    {
        _logger.Log(LogLevel.Information, request.ToString());
        return await _userHelper.LoginUser(request);
    }


    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        // Удаляем куки аутентификации
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Вернем сообщение об успешном выходе
        return Ok("Logged out successfully");
    }
}