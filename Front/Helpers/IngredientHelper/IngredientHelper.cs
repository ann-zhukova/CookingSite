using AutoMapper;
using Core.IoC;
using DataAccess.Ingredients;
using Front.Models.Ingredient;
using JetBrains.Annotations;

namespace Front.Helpers.IngredientHelper;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
public class IngredientHelper : IIngredientHelper
{
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IMapper _mapper;
    
    public IngredientHelper(IIngredientsRepository ingredientsRepository, IMapper mapper)
    {
        _ingredientsRepository = ingredientsRepository;
        _mapper = mapper;
    }
    
    public async Task<IngredientsResponseJsModel> GetIngredients()
    {
        var ingredients = await _ingredientsRepository.GetIngredientsAsync();
        return new(){Ingredients= _mapper.Map<List<IngredientResponseJs>>(ingredients)};
    }
}