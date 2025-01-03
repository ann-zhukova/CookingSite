using Domain.Recipes;

namespace Domain.Ingredients;

public class Ingredient
{
    public Guid Id { get; set; }
    public string IngredientName { get; set; }
    
    public List<Recipe> Recipes { get; set; }
}