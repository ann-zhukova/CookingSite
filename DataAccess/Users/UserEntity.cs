using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core;
using DataAccess.Base;
using DataAccess.Recipes;
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
    
    [StringLength(Constants.EmailLength)]
    [Required]
    public string? Email { get; set; }

    [StringLength(Constants.PasswordLength)]
    [Required]
    public required string Password { get; set; }
    
    [InverseProperty(nameof(RecipeEntity.User))]
    public ICollection<RecipeEntity> Recipes { get; set; }
    
    [InverseProperty(nameof(RecipeEntity.UserFavorites))]
    public ICollection<RecipeEntity> FavoriteRecipes { get; set; }
}