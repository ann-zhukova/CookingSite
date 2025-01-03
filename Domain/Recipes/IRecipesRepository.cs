using Domain.Recipes;
using JetBrains.Annotations;

namespace DataAccess.Recipes;

public interface IRecipesRepository
{
    Task<IReadOnlyCollection<Recipe>> GetRecipesAsync();
    Task<Recipe> GetRecipeByIdAsync(Guid id);
    Task<IReadOnlyCollection<Recipe>> GetRecipeByUserId(Guid userId);
    Task<Guid> CreateRecipeAsync([NotNull] Recipe recipe);
    Task<Guid?> UpdateRecipeAsync([NotNull] Recipe recipe);
    Task DeleteRecipeAsync(Guid id);
}