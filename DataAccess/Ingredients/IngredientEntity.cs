using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Base;
using DataAccess.Recipes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Ingredients;

[Table("Ingredients")]
[Index(nameof(IngredientName), IsUnique = true)]
internal sealed class  IngredientEntity: BaseEntity
{
    [Required]
    public string IngredientName { get; set; }
    
    [InverseProperty(nameof(RecipeEntity.Ingredients))]
    public List<RecipeEntity> Recipes { get; set; }
}