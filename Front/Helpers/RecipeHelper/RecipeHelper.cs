using AutoMapper;
using Core.IoC;
using Core;
using Domain;
using Domain.Users;
using JetBrains.Annotations;
using DataAccess.Base;
using DataAccess.Ingredients;
using DataAccess.Recipes;
using DataAccess.Steps;
using DataAccess.Types;
using Domain.Recipes;
using Domain.Steps;
using Front.Helpers.IngredientHelper;
using Front.Models.Recipe;

namespace Front.Helpers.RecipeHelper;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
public class RecipeHelper : IRecipeHelper
{
    private readonly IRecipesRepository _recipeRepository;
    private readonly IStepsRepository _stepsRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITypesRepository _typesRepository;
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IMapper _mapper;

    public RecipeHelper(IRecipesRepository recipeRepository, IMapper mapper, IUserRepository userRepository,
        IStepsRepository stepsRepository,
        ITypesRepository typesRepository,
        IIngredientsRepository ingredientsRepository)
    {
        _recipeRepository = recipeRepository;
        _userRepository = userRepository;
        _stepsRepository = stepsRepository;
        _ingredientsRepository = ingredientsRepository;
        _typesRepository = typesRepository;
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
    
    public async Task<RecipeResponseJsModel> CreateRecipe(RecipeRequestJs recipeRequest)
    {
        // Создаем шаги асинхронно и дожидаемся их выполнения
        var stepsGuid = await Task.WhenAll(recipeRequest.Steps
            .Select(async s => await _stepsRepository.CreateStepAsync(_mapper.Map<Step>(s))));
        
        var steps = await Task.WhenAll(stepsGuid
            .Select(async id => await _stepsRepository.GetStepByIdAsync(id)));

        // Получаем типы асинхронно и дожидаемся их выполнения
        var types = await Task.WhenAll(recipeRequest.Types
            .Select(async t => await _typesRepository.GetTypeByIdAsync(t.Id)));

        // Получаем ингредиенты асинхронно и дожидаемся их выполнения
        var ingredients = await Task.WhenAll(recipeRequest.Ingredients
            .Select(async i => await _ingredientsRepository.GetIngredientByIdAsync(i.Id)));

        // Маппим и заполняем рецепт
        var recipe = _mapper.Map<Recipe>(recipeRequest);
        recipe.Steps = steps.ToList();
        recipe.Types = types.ToList();
        recipe.Ingredients = ingredients.ToList();
        recipe.Id = Guid.NewGuid();

        // Создаем рецепт и проверяем результат
        var id = await _recipeRepository.CreateRecipeAsync(recipe);
        if (id == null)
        {
            return new RecipeResponseJsModel
            {
                ErrorCode = Core.Constants.ErrorCode.Forbidden,
                ErrorDetail = "Не удалось создать рецепт"
            };
        }

        // Возвращаем успешный ответ
        return new ();
    }

    public async Task<RecipeResponseJsModel> AddToFavorites(Guid id, Guid userId)
    {
        var recipes = await _recipeRepository.GetRecipesAsync();
        return new();
    }
}