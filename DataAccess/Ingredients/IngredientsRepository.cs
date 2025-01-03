using AutoMapper;
using Core.IoC;
using DataAccess.Base;
using Domain.Ingredients;
using Domain.Recipes;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Ingredients;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
internal sealed class IngredientsRepository(PostgresContext context, IMapper mapper)
    : BaseRepository(context, mapper)
{
    public async Task<IReadOnlyCollection<Ingredient>> GetIngredientsAsync()
    {
        var recipes = await Context.Ingredients.AsNoTracking().ToListAsync();
        return Mapper.Map<Ingredient[]>(recipes);
    }

    public async Task<Ingredient> GetIngredientByIdAsync(Guid id)
    {
        var recipe = await Context.Ingredients.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);
        return Mapper.Map<Ingredient>(recipe);
    }

    public async Task<Guid> CreateIngredientAsync([NotNull] Ingredient ingredient)
    {
        ingredient.Id = Guid.NewGuid();
        await Context.Ingredients.AddAsync(Mapper.Map<IngredientEntity>(ingredient));
        return ingredient.Id;
    }

    public async Task<Guid?> UpdateIngredientAsync([NotNull] Ingredient ingredient)
    {
        var ingredientEntity = await Context.Ingredients.SingleOrDefaultAsync(u => u.Id == ingredient.Id);

        if (ingredientEntity == null)
        {
            return null;
        }

        ingredientEntity.IngredientName = ingredient.IngredientName;
        
        return ingredientEntity.Id;
    }

    public async Task DeleteIngredientAsync(Guid id)
    {
        await Context.Ingredients.Where(u => u.Id == id).ExecuteDeleteAsync();
    }
}