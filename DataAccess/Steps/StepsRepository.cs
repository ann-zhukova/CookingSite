using AutoMapper;
using Core.IoC;
using DataAccess.Base;
using DataAccess.Type;
using Domain.Steps;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Steps;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
internal sealed class StepsRepository(PostgresContext context, IMapper mapper)
    : BaseRepository(context, mapper), IStepsRepository
{
    public async Task<IReadOnlyCollection<Step>> GetStepsAsync()
    {
        var users = await Context.Steps.AsNoTracking().ToListAsync();
        return Mapper.Map<Step[]>(users);
    }
    public async Task<IReadOnlyCollection<Step>> GetStepsAsync(ICollection<Guid> steps)
    {
        var stepsEntity = await Context.Steps
            .AsNoTracking()
            .Where(s=> steps.Contains(s.Id))
            .ToListAsync();
        return Mapper.Map<List<Step>>(stepsEntity);
    }
    public async Task<Step> GetStepByIdAsync(Guid id)
    {
        var step = await Context.Steps.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
        return Mapper.Map<Step>(step);
    }

    public async Task<IReadOnlyCollection<Step>> GetStepByRecipeId(Guid recipeId)
    {
        var steps = await Context.Steps.AsNoTracking().Select(u => u.Recipe).Where(r => r.Id == recipeId).ToListAsync();
        return steps.Select(s => Mapper.Map<Step>(s)).ToList();
    }

    public async Task<Guid> CreateStepAsync([NotNull] Step step)
    {
        step.Id = Guid.NewGuid();
        await Context.Steps.AddAsync(Mapper.Map<StepEntity>(step));
        return step.Id;
    }

    public async Task<Guid?> UpdateStepAsync([NotNull] Step step)
    {
        var stepEntity = await Context.Steps.SingleOrDefaultAsync(u => u.Id == step.Id);

        if (stepEntity == null)
        {
            return null;
        }

        stepEntity.StepNumber = step.StepNumber;
        stepEntity.StepDescription = step.StepDescription;
        stepEntity.RecipetId = step.RecipetId;
        
        return stepEntity.Id;
    }

    public async Task DeleteStepAsync(Guid id)
    {
        await Context.Steps.Where(u => u.Id == id).ExecuteDeleteAsync();
    }
}