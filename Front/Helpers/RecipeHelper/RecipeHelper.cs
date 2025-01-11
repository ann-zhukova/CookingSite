using AutoMapper;
using Core.IoC;
using Core;
using Domain;
using Domain.Users;
using JetBrains.Annotations;
using DataAccess.Base;
using DataAccess.Recipes;
using Domain.Recipes;
using Front.Models.Recipe;

namespace Front.Helpers.RecipeHelper;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
public class RecipeHelper : IRecipeHelper
{
    private readonly IRecipesRepository _recipeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RecipeHelper(IRecipesRepository recipeRepository, IMapper mapper, IUserRepository userRepository)
    {
        _recipeRepository = recipeRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<RecipesResponseJsModel> GetRecipes()
    {
        var serials = await _recipeRepository.GetRecipesAsync();
        return new(){Recipes= _mapper.Map<List<RecipeResponseJs>>(serials)};
    }

    public async Task<RecipesResponseJsModel> GetRecipes(RecipeFilter filter)
    {
        var serials = await _recipeRepository.GetRecipesAsync(filter);
        var totalItems = await _recipeRepository.CountRecipesAsync(filter);

        return new RecipesResponseJsModel
        {
            Recipes = _mapper.Map<List<RecipeResponseJs>>(serials),
            CurrentPage = filter.Page,
            TotalPages = (int)Math.Ceiling((double)totalItems / filter.PageSize),
            TotalItems = totalItems
        };
    }
    
    public async Task<RecipeDetailsResponseJsModel> GetRecipe(Guid id)
    {
        var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
        return recipe == null
            ? new() { ErrorCode = Core.Constants.ErrorCode.Forbidden }
            : new() {Recipe = _mapper.Map<RecipeDetailsResponseJs>(recipe) };
    }

    public async Task<RecipeResponseJsModel> AddToFavorites(Guid id, Guid userId)
    {
        var recipes = await _recipeRepository.GetRecipesAsync();
        return new();
    }
}