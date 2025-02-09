using Domain.Base;
using Domain.Steps;
using JetBrains.Annotations;

namespace DataAccess.Steps;

public interface IStepsRepository: IBaseRepository
{
    Task<IReadOnlyCollection<Step>> GetStepsAsync();
    Task<IReadOnlyCollection<Step>> GetStepsAsync(ICollection<Guid> steps);
    Task<Step> GetStepByIdAsync(Guid id);
    Task<IReadOnlyCollection<Step>> GetStepByRecipeId(Guid recipeId);
    Task<Guid> CreateStepAsync([NotNull] Step step);
    Task<Guid?> UpdateStepAsync([NotNull] Step step);
    Task DeleteStepAsync(Guid id);
}