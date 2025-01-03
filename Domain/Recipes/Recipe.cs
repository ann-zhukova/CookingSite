using Domain.Ingredients;
using Domain.Steps;
using Domain.Users;

namespace Domain.Recipes;

public class Recipe
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public int PrepareTime { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    public ICollection<Ingredient> Ingredients { get; set; }
    
    public ICollection<Step> Steps { get; set; }
    
    public ICollection<Domain.Types.Type> Types { get; set; }
    
    public ICollection<User> UserFavorites { get; set; }
}