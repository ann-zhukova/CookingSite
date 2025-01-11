using System.ComponentModel.DataAnnotations;

namespace Front.Models.User;

public class UserRegisterRequestJs
{
    [StringLength(Core.Constants.MaxLoginLength, MinimumLength = Core.Constants.MinLoginLength)]
    public string UserName { get; set; }
    
    [StringLength(Core.Constants.MaxEmailLength, MinimumLength = Core.Constants.MinEmailLength)]
    public string Email { get; set; }
    
    [StringLength(Core.Constants.MaxPasswordLength, MinimumLength = Core.Constants.MinPasswordLength)]
    public string Password { get; set; }
    [StringLength(Core.Constants.MaxPasswordLength, MinimumLength = Core.Constants.MinPasswordLength)]
    public string RepetedPassword { get; set; }
}