using Domain.Base;
using Domain.Ingredients;
using JetBrains.Annotations;

namespace DataAccess.Ingredients;

public interface IIngredientsRepository: IBaseRepository
{
    Task<IReadOnlyCollection<Ingredient>> GetIngredientsAsync();
    Task<IReadOnlyCollection<Ingredient>> GetIngredientsAsync(ICollection<Guid> ingredients);
    Task<Ingredient> GetIngredientByIdAsync(Guid id);
    Task<Guid> CreateIngredientAsync([NotNull] Ingredient ingredient);
    Task<Guid?> UpdateIngredientAsync([NotNull] Ingredient ingredient);
    Task DeleteIngredientAsync(Guid id);
}