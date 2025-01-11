using Core.Extensions;
using Front.Helpers.TypeHelper;
using Microsoft.AspNetCore.Mvc;
using Front.Extensions;

namespace Front.Controllers;

[Route("types")]
public class TypeController: Controller
{
    private readonly ITypeHelper _typeHelper;

    public TypeController(ITypeHelper typeHelper)
    {
        _typeHelper = typeHelper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTypes()
    {
        return await _typeHelper.GetTypes().Convert(this.ToActionResult);
    }
}