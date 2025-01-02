using Domain.Recipes;

namespace Domain.Types;

public class Type
{
    public Guid Id { get; set; }
    public string TypeName { get; set; }
    
    public ICollection<Recipe> Recipes { get; set; }
}