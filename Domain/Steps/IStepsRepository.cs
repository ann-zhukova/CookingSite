using Domain.Steps;
using JetBrains.Annotations;

namespace DataAccess.Steps;

public interface IStepsRepository
{
    Task<IReadOnlyCollection<Step>> GetStepsAsync();
    Task<Step> GetStepByIdAsync(Guid id);
    Task<IReadOnlyCollection<Step>> GetStepByRecipeId(Guid recipeId);
    Task<Guid> CreateStepAsync([NotNull] Step step);
    Task<Guid?> UpdateStepAsync([NotNull] Step step);
    Task DeleteStepAsync(Guid id);
}