using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Base;
using DataAccess.Recipes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Steps;

[Table("Steps")]
[Index(nameof(RecipetId), nameof(StepNumber), IsUnique = true)]
internal sealed class  StepEntity: BaseEntity
{
    public int StepNumber { get; set; }
    
    public string StepDescription { get; set; }
    
    public Guid RecipetId { get; set; }
    
    [ForeignKey("RecipetId")]
    [InverseProperty(nameof(RecipeEntity.Steps))]
    public RecipeEntity Recipe { get; set; }
    
}