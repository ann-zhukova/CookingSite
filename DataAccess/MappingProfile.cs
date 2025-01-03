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
using JetBrains.Annotations;
namespace DataAccess;

[UsedImplicitly]
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserEntity>();
        CreateMap<UserEntity, User>();
        CreateMap<Domain.Types.Type, TypeEntity>();
        CreateMap<TypeEntity, Domain.Types.Type>();
        CreateMap<Step, StepEntity>();
        CreateMap<StepEntity, Step>();
        CreateMap<Recipe, RecipeEntity>();
        CreateMap<RecipeEntity, Recipe>();
        CreateMap<Ingredient, IngredientEntity>();
        CreateMap<IngredientEntity, Ingredient>();
    }
}