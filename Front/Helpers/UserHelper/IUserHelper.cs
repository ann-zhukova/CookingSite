using Front.Models.Recipe;
using Front.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Front.Helpers.UserHelper;

public interface IUserHelper
{
    Task<LoginResponseJsModel> RegisterUser(UserRegisterRequestJs request);
    Task<LoginResponseJsModel> LoginUser(UserLoginRequestJs request);

    Task<UserResponseJsModel> Account(String userName);

    public  Task<FavoritesResponseJsModel> Favorites(String userName);
    public Task<RecipesResponseJsModel> UserRecipes(String userName);
}