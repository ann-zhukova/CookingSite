using Front.Models.Ingredient;
using Front.Models.Step;
using Front.Models.Type;

namespace Front.Models.Recipe;

public class RecipeDetailsResponseJs
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public int PrepareTime { get; set; }
    
    public int YourTime { get; set; }
    
    public string Image { get; set; }
    
    public ICollection<IngredientResponseJs> Ingredients { get; set; }
    
    public ICollection<StepResponseJs> Steps { get; set; }
    
    public ICollection<TypeResponseJs> Types { get; set; }
    
}