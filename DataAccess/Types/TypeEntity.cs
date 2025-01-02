using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using DataAccess.Base;
using DataAccess.Recipes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Type;

[Table("Types")]
[Index(nameof(TypeName), IsUnique = true)]
internal sealed class  TypeEntity: BaseEntity
{
    [Required]
    public string TypeName { get; set; }
    
    [InverseProperty(nameof(RecipeEntity.Types))]
    public ICollection<RecipeEntity> Recipes { get; set; }
}