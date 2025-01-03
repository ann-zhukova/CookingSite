using AutoMapper;
using Core.IoC;
using DataAccess.Base;
using Domain.Recipes;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Recipes;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
internal sealed class RecipesRepository(PostgresContext context, IMapper mapper)
    : BaseRepository(context, mapper)
{
    public async Task<IReadOnlyCollection<Recipe>> GetRecipesAsync()
    {
        var recipes = await Context.Recipes.AsNoTracking().ToListAsync();
        return Mapper.Map<Recipe[]>(recipes);
    }

    public async Task<Recipe> GetRecipeByIdAsync(Guid id)
    {
        var recipe = await Context.Recipes.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);
        return Mapper.Map<Recipe>(recipe);
    }

    public async Task<IReadOnlyCollection<Recipe>> GetRecipeByUserId(Guid userId)
    {
        var steps = await Context.Recipes.AsNoTracking().Select(r=> r).Where(r => r.UserId == userId).ToListAsync();
        return steps.Select(s => Mapper.Map<Recipe>(s)).ToList();
    }

    public async Task<Guid> CreateRecipeAsync([NotNull] Recipe recipe)
    {
        recipe.Id = Guid.NewGuid();
        await Context.Recipes.AddAsync(Mapper.Map<RecipeEntity>(recipe));
        return recipe.Id;
    }

    public async Task<Guid?> UpdateRecipeAsync([NotNull] Recipe recipe)
    {
        var recipeEntity = await Context.Recipes.SingleOrDefaultAsync(u => u.Id == recipe.Id);

        if (recipeEntity == null)
        {
            return null;
        }

        recipeEntity.Name = recipe.Name;
        recipeEntity.PrepareTime = recipe.PrepareTime;
        recipeEntity.UserId = recipe.UserId;
        
        var userFavorites = await Context.Users
            .Where(u => recipe.UserFavorites.Select(f=>f.Id).Contains(u.Id))
            .ToListAsync();
        recipeEntity.UserFavorites.Clear();
        foreach (var userEntity in userFavorites)
        {
            if (recipeEntity.UserFavorites.All(u => u.Id != userEntity.Id))
            {
                recipeEntity.UserFavorites.Add(userEntity);
            }
        }
        
        var types = await Context.Types
            .Where(t => recipe.Types.Select(r=>r.Id).Contains(t.Id)).
            ToListAsync();
        recipeEntity.Types.Clear();
        foreach (var type in types)
        {
            if (recipeEntity.Types.All(u => u.Id != type.Id))
            {
                recipeEntity.Types.Add(type);
            }
        }
        
        var steps = await Context.Steps
            .Where(s => recipe.Steps.Select(r => r.Id).Contains(s.Id))
            .ToListAsync();
        recipeEntity.Steps.Clear();
        foreach (var step in steps)
        {
            if (recipeEntity.Steps.All(u => u.Id != step.Id))
            {
                recipeEntity.Steps.Add(step);
            }
        }
        
        var ingredients = await Context.Ingredients
            .Where(i => recipe.Ingredients.Select(r => r.Id).Contains(i.Id))
            .ToListAsync();
        recipeEntity.Ingredients.Clear();
        foreach (var ingredient in ingredients)
        {
            if (recipeEntity.Ingredients.All(u => u.Id != ingredient.Id))
            {
                recipeEntity.Ingredients.Add(ingredient);
            }
        }
        
        return recipeEntity.Id;
    }

    public async Task DeleteStepAsync(Guid id)
    {
        await Context.Recipes.Where(u => u.Id == id).ExecuteDeleteAsync();
    }
}