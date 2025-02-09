using Domain.Base;
using JetBrains.Annotations;

namespace Domain.Recipes;

public interface IRecipesRepository: IBaseRepository
{
    Task<IReadOnlyCollection<Recipe>> GetRecipesAsync();
    Task<Recipe> GetRecipeByIdAsync(Guid id);
    Task<IReadOnlyCollection<Recipe>> GetRecipeByUserId(Guid userId);
    public Task<int> CountRecipesAsync(RecipeFilter filter);
    public Task<IReadOnlyCollection<Recipe>> GetRecipesAsync(RecipeFilter filter);
    Task<Guid> CreateRecipeAsync([NotNull] Recipe recipe);
    Task<Guid?> UpdateRecipeAsync([NotNull] Recipe recipe);
    Task DeleteRecipeAsync(Guid id);
}