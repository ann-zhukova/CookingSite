using AutoMapper;
using DataAccess.Ingredients;
using DataAccess.Recipes;
using DataAccess.Steps;
using DataAccess.Type;
using DataAccess.Users;
using Domain.Ingredients;
using Domain.Recipes;
using Domain.Steps;
using Domain.Users;
using Front.Models.Ingredient;
using Front.Models.Recipe;
using Front.Models.Step;
using Front.Models.Type;
using Front.Models.User;
using JetBrains.Annotations;

namespace Front;

[UsedImplicitly]
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserResponseJs, User>();
        CreateMap<UserResponseJs, User>();
        CreateMap<Domain.Types.Type, TypeResponseJs>();
        CreateMap<TypeResponseJs, Domain.Types.Type>();
        CreateMap<Step, StepResponseJs>();
        CreateMap<StepResponseJs, Step>();
        CreateMap<Recipe, RecipeResponseJs>();
        CreateMap<RecipeResponseJs, Recipe>();
        CreateMap<Recipe, RecipeDetailsResponseJs>();
        CreateMap<RecipeDetailsResponseJs, Recipe>();
        CreateMap<Ingredient, IngredientResponseJs>();
        CreateMap<IngredientResponseJs, Ingredient>();
    }
}