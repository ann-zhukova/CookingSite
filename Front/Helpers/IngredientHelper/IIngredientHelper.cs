using Front.Models.Ingredient;

namespace Front.Helpers.IngredientHelper;

public interface IIngredientHelper
{
    public Task<IngredientsResponseJsModel> GetIngredients();
}