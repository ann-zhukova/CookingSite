using Front.Models.Ingredient;
using Front.Models.Step;
using Front.Models.Type;

namespace Front.Models.Recipe;

public class RecipeRequestJs
{

    public string Name { get; set; }
    
    public int PrepareTime { get; set; }
    
    public int YourTime { get; set; }
    
    public string Image { get; set; }
    
    public ICollection<Guid> Ingredients { get; set; }
    
    public ICollection<StepRequestJs> Steps { get; set; }
    
    public ICollection<Guid> Types { get; set; }
    
}