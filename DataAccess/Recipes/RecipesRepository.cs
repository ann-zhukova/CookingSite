using AutoMapper;
using Core.IoC;
using DataAccess.Base;
using DataAccess.Ingredients;
using DataAccess.Steps;
using DataAccess.Type;
using DataAccess.Users;
using Domain;
using Domain.Recipes;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Recipes;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
internal sealed class RecipesRepository(PostgresContext context, IMapper mapper)
    : BaseRepository(context, mapper), IRecipesRepository
{
    public async Task<IReadOnlyCollection<Recipe>> GetRecipesAsync()
    {
        var recipes = await Context.Recipes
            .AsNoTracking()
            .Include(r=>r.Ingredients)
            .Include(r=>r.Types)
            .ToListAsync();
        return Mapper.Map<Recipe[]>(recipes);
    }
    
    public async Task<IReadOnlyCollection<Recipe>> GetRecipesAsync(RecipeFilter filter)
    {
        var query = Context.Recipes
            .Include(s => s.Types)
            .Include(r=>r.Ingredients)
            .AsNoTracking()
            .AsQueryable();
        
        if (filter.MinTime.HasValue)
        {
            query = query.Where(s => s.PrepareTime >= filter.MinTime.Value);
        }

        if (filter.MaxTime.HasValue)
        {
            query = query.Where(s => s.PrepareTime <= filter.MaxTime.Value);
        }

        if (filter.Types != null && filter.Types.Any())
        {
            query = query.Where(s => s.Types.Any(t => filter.Types.Contains(t.Id)));
        }
        if (filter.Ingredients != null && filter.Ingredients.Any())
        {
            query = query.Where(s => s.Ingredients.Any(t => filter.Ingredients.Contains(t.Id)));
        }
        
        // Сортировка в зависимости от выбранного параметра
        switch (filter.SortBy)
        {
            case "yourTime":
                query = query.OrderBy(s => s.YourTime);
                break;
            case "prepareTime":
                query = query.OrderByDescending(s => s.PrepareTime);
                break;
            default:
                query = query.OrderBy(s => s.Id); // по умолчанию сортируем по id
                break;
        }
        // Пагинация
        var skip = (filter.Page - 1) * filter.PageSize;
        query = query
            .Skip(skip)
            .Take(filter.PageSize);

        var recipes = await query.ToListAsync();
        return Mapper.Map<Recipe[]>(recipes);
    }

    public async Task<int> CountRecipesAsync(RecipeFilter filter)
    {
        var query = Context.Recipes.AsQueryable();
        
        if (filter.MinTime.HasValue)
        {
            query = query.Where(s => s.PrepareTime >= filter.MinTime.Value);
        }

        if (filter.MaxTime.HasValue)
        {
            query = query.Where(s => s.PrepareTime <= filter.MaxTime.Value);
        }

        if (filter.Types != null && filter.Types.Any())
        {
            query = query.Where(s => s.Types.Any(t => filter.Types.Contains(t.Id)));
        }
        if (filter.Ingredients != null && filter.Ingredients.Any())
        {
            query = query.Where(s => s.Ingredients.Any(t => filter.Ingredients.Contains(t.Id)));
        }

        return await query.CountAsync();
    }

    public async Task<Recipe> GetRecipeByIdAsync(Guid id)
    {
        var recipe = await Context
            .Recipes
            .AsNoTracking()
            .Include(r=>r.Ingredients)
            .Include(r=>r.Types)
            .Include(r=>r.Steps)
            .Include(r=>r.UserFavorites)
            .SingleOrDefaultAsync(r => r.Id == id);
        return Mapper.Map<Recipe>(recipe);
    }

    public async Task<IReadOnlyCollection<Recipe>> GetRecipeByUserId(Guid userId)
    {
        var steps = await Context.Recipes.AsNoTracking().Select(r=> r).Where(r => r.UserId == userId).ToListAsync();
        return steps.Select(s => Mapper.Map<Recipe>(s)).ToList();
    }

    public async Task<Guid> CreateRecipeAsync([NotNull] Recipe recipe)
    {
        var recipeEntity = new RecipeEntity()
        {
            Image = recipe.Image,
            Name = recipe.Name,
            PrepareTime = recipe.PrepareTime,
            UserId = recipe.UserId,
            YourTime = recipe.YourTime,
            Types = new List<TypeEntity>(),
            Steps = new List<StepEntity>(),
            Ingredients = new List<IngredientEntity>(),
        };
        var types = await Context.Types
            .Where(g => recipe.Types.Select(s => s.Id).Contains(g.Id))
            .ToListAsync();
        foreach (var type in types)
        {
            recipeEntity.Types.Add(type);
        }
        var ingredients = await Context.Ingredients
            .Where(g => recipe.Ingredients.Select(s => s.Id).Contains(g.Id))
            .ToListAsync();
        foreach (var ingredient in ingredients)
        {
            recipeEntity.Ingredients.Add(ingredient);
        }
        await Context.Recipes
            .AddAsync(recipeEntity);
        return recipeEntity.Id;
    }

    public async Task<Guid?> UpdateRecipeAsync([NotNull] Recipe recipe)
    {
        var recipeEntity = await Context.Recipes
            .Include(r=>r.Ingredients)
            .Include(r=>r.UserFavorites)
            .Include(r=>r.Types)
            .Include(r=>r.Steps)
            .SingleOrDefaultAsync(u => u.Id == recipe.Id);

        if (recipeEntity == null)
        {
            return null;
        }

        recipeEntity.Name = recipe.Name;
        recipeEntity.PrepareTime = recipe.PrepareTime;
        recipeEntity.UserId = recipe.UserId;
        recipeEntity.Image = recipe.Image;
        recipeEntity.YourTime = recipe.YourTime;
        
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

    public async Task DeleteRecipeAsync(Guid id)
    {
        await Context.Recipes.Where(u => u.Id == id).ExecuteDeleteAsync();
    }
}