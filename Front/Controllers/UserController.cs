using System.Security.Claims;
using Core.Extensions;
using Front.Helpers.UserHelper;
using Front.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Front.Extensions;

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
    
    [HttpPost]
    [AllowAnonymous]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestJs request)
    {
        _logger.Log(LogLevel.Information, request.ToString());
        return await _userHelper.RegisterUser(request).Convert(this.ToActionResult);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public Task<IActionResult> Login([FromBody] UserLoginRequestJs request)
    {
        return _userHelper.LoginUser(request).Convert(this.ToActionResult);
    }

    [HttpPost]
    [Authorize]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        // Удаляем куки аутентификации
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Вернем сообщение об успешном выходе
        return Ok("Logged out successfully");
    }

    [HttpGet]
    [Authorize]
    [Route("account")]
    public async Task<IActionResult> Account()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        return await _userHelper.Account(userName).Convert(this.ToActionResult);
    }
    
    [HttpGet]
    [Route("recipes")]
    public async Task<IActionResult> GetUserRecipe(Guid id)
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        return await _userHelper.UserRecipes(userName).Convert(this.ToActionResult);
    }
    
    [HttpGet]
    [Route("recipes/favorites")]
    public async Task<IActionResult> GetFavoritesRecipe(Guid id)
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        return await _userHelper.Favorites(userName).Convert(this.ToActionResult);
    }
}