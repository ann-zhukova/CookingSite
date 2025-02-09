using Domain;
using Front.Models.Recipe;

namespace Front.Helpers.RecipeHelper;

public interface IRecipeHelper
{
    Task<RecipesResponseJsModel> GetRecipes();
    Task<RecipesResponseJsModel> GetRecipes(RecipeFilter filter);
    Task<RecipeDetailsResponseJsModel> GetRecipe(Guid id);

    Task<RecipeResponseJsModel> CreateRecipe(RecipeRequestJs recipeRequest, String username);
    Task<RecipeResponseJsModel> AddToFavorites(Guid id, String userName);
}