using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Base;
using DataAccess.Ingredients;
using DataAccess.Steps;
using DataAccess.Type;
using DataAccess.Users;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Recipes;

[Table("Recipes")]
[Index(nameof(PrepareTime), IsUnique = false)]
internal sealed class  RecipeEntity: BaseEntity
{
    [Required]
    public string Name { get; set; }
    
    public int PrepareTime { get; set; }
    
    public int YourTime { get; set; }
    
    public string Image { get; set; }
    
    public Guid UserId { get; set; }
    
    [ForeignKey("UserId")]
    public UserEntity User { get; set; }
    
    [InverseProperty(nameof(IngredientEntity.Recipes))]
    public ICollection<IngredientEntity> Ingredients { get; set; }
    
    [InverseProperty(nameof(StepEntity.Recipe))]
    public ICollection<StepEntity> Steps { get; set; }
    
    [InverseProperty(nameof(TypeEntity.Recipes))]
    public ICollection<TypeEntity> Types { get; set; }
    
    [InverseProperty(nameof(UserEntity.FavoriteRecipes))]
    public ICollection<UserEntity> UserFavorites { get; set; }
}