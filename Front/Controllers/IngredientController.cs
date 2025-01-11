using Core.Extensions;
using Front.Helpers.IngredientHelper;
using Microsoft.AspNetCore.Mvc;
using Front.Extensions;

namespace Front.Controllers;

[Route("ingredients")]
public class IngredientController: Controller
{
    private readonly IIngredientHelper _ingredientHelper;

    public IngredientController(IIngredientHelper ingredientHelper)
    {
        _ingredientHelper = ingredientHelper;
    }
    [HttpGet]
    public async Task<IActionResult> GetRecipes()
    {
        return await _ingredientHelper.GetIngredients().Convert(this.ToActionResult);
    }
}