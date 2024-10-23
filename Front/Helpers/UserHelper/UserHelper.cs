using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Core.IoC;
using Domain.Users;
using Front.Models.User;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Front.Helpers.UserHelper;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
public class UserHelper : IUserHelper
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserHelper(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult OkResponse(object value)
    {
        return new OkObjectResult(value);
    }

    public IActionResult BadRequestResponse(string message)
    {
        return new BadRequestObjectResult(message);
    }

    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public async Task<IActionResult> RegisterUser(UserRegisterRequestJs request)
    {
        var hashedPassword = HashPassword(request.Password);
        var newUser = new User(request.UserName,  hashedPassword, true);

        var newUserGuid = await _userRepository.CreateUserAsync(newUser);
        await _userRepository.SaveChangesAsync();
        if (newUserGuid == Guid.Empty)
        {
            return BadRequestResponse("Invalid username");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(ClaimTypes.Role, UserRoles.User.ToString()) //todo admin or player
        };

        // создаем объект ClaimsIdentity
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // установка аутентификационных куки
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return OkResponse(newUserGuid);
    }

    public async Task<IActionResult> LoginUser(UserLoginRequestJs request)
    {
        var user = await _userRepository.GetUserByNameAsync(request.UserName);
        if (user.Id == Guid.Empty)
        {
            return BadRequestResponse("Пользователя не сузествует");
        }

        if (HashPassword(request.Password) != user.Password)
            return BadRequestResponse("Wrong password");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, UserRoles.User.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return OkResponse(user.Id);
    }
}