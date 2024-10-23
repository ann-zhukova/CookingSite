using Front.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Front.Helpers.UserHelper;

public interface IUserHelper
{
    Task<IActionResult> RegisterUser(UserRegisterRequestJs request);
    Task<IActionResult> LoginUser(UserLoginRequestJs request);
}