using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Core.IoC;
using Domain.Users;
using Front.Models.Recipe;
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
    
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

 public async Task<LoginResponseJsModel>  RegisterUser(UserRegisterRequestJs request)
    {
        var hashedPassword = HashPassword(request.Password);
        var newUser = new User(request.UserName,  hashedPassword, request.Email);
        var user = await _userRepository.GetUserByNameAsync(request.UserName);
        if (!(user is null))
        {
            return new() { ErrorCode = Core.Constants.ErrorCode.Forbidden, ErrorDetail = "Пользователь уже существует" };
        }
        var newUserGuid = await _userRepository.CreateUserAsync(newUser);
        await _userRepository.SaveChangesAsync();
        if (newUserGuid == Guid.Empty)
        {
            return new() { ErrorCode = Core.Constants.ErrorCode.Forbidden, ErrorDetail = "Ошибка при регистрации" };
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(ClaimTypes.Role, UserRoles.User.ToString()) 
        };

        // создаем объект ClaimsIdentity
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // установка аутентификационных куки
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return new();
    }

    public async Task<LoginResponseJsModel> LoginUser(UserLoginRequestJs request)
    {
        var user = await _userRepository.GetUserByNameAsync(request.UserName);
        if (user is null)
        {
            return new() { ErrorCode = Core.Constants.ErrorCode.Forbidden, ErrorDetail = "Пользователь не найден" };
        }

        if (HashPassword(request.Password) != user.Password)
            return new() { ErrorCode = Core.Constants.ErrorCode.Forbidden, ErrorDetail = "Неверный пароль" };


        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, UserRoles.User.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return new();
    }

    public async Task<UserResponseJsModel> Account(String userName)
    {

        var user = await _userRepository.GetUserByNameAsync(userName);
        if (user is null)
            return new() { ErrorCode = Core.Constants.ErrorCode.Forbidden, ErrorDetail = "Пользователь не найден" };
        var userAccount = await _userRepository.GetUserByIdAsync(user.Id);
        if (userAccount is null)
            return new() { ErrorCode = Core.Constants.ErrorCode.Forbidden, ErrorDetail = "Ошибка получения данных"};
        return new UserResponseJsModel() {
            UserName = userAccount.UserName,
        };
    }

    public async Task<FavoritesResponseJsModel> Favorites(String userName)
    {
        var user = await _userRepository.GetUserFavoritesAsync(userName);
        if (user is null)
            return new() { ErrorCode = Core.Constants.ErrorCode.Forbidden, ErrorDetail = "Пользователь не найден" };
        return new FavoritesResponseJsModel() {
             Recipes = user.FavoriteRecipes.Select(u => _mapper.Map<RecipeResponseJs>(u)).ToList(),
        };
    }
    
    public async Task<RecipesResponseJsModel> UserRecipes(String userName)
    {
        var user = await _userRepository.GetUserRecipesAsync(userName);
        if (user is null)
            return new() { ErrorCode = Core.Constants.ErrorCode.Forbidden, ErrorDetail = "Пользователь не найден" };
        
        return new RecipesResponseJsModel() {
            Recipes = user.Recipes.Select(u => _mapper.Map<RecipeResponseJs>(u)).ToList(),
        };
    }
}