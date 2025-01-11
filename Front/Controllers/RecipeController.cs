using Microsoft.AspNetCore.Mvc;
using Core.Extensions;
using Domain;
using Front.Extensions;
using Front.Helpers.RecipeHelper;
using Front.Models.Recipe;

namespace Front.Controllers;

[Route("recipes")]
public class RecipeController: Controller
{
    private readonly IRecipeHelper _recipeHelper;

    public RecipeController(IRecipeHelper recipeHelper)
    {
        _recipeHelper = recipeHelper;
    }
    [HttpGet]
    public async Task<IActionResult> GetRecipes([FromQuery] RecipeFilter filter)
    {
        return await _recipeHelper.GetRecipes(filter).Convert(this.ToActionResult);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetRecipe(Guid id)
    {
        return await _recipeHelper.GetRecipe(id).Convert(this.ToActionResult);
    }
    
}