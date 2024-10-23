using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core;
using DataAccess.Base;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Users;

[Table("Users")]
[Index(nameof(UserName), IsUnique = true)]
[Index(nameof(UserName), nameof(Password), IsUnique = true)]
internal sealed class UserEntity : BaseEntity
{
    [StringLength(Constants.UserNameLength)]
    [Required]
    public required string UserName { get; set; }
    

    [StringLength(Constants.PasswordLength)]
    [Required]
    public required string Password { get; set; }
    
}