namespace Front.Models.Ingredient;

public class IngredientsResponseJsModel: BaseResponseJsModel
{
    public ICollection<IngredientResponseJs> Ingredients { get; set; }
}