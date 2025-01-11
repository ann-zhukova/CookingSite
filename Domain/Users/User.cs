
using Domain.Recipes;

namespace Domain.Users;

public class User(string userName, string password, string email)
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; } = userName;
    
    public string? Email { get; set; } = email;

    public string Password { get; set; } = password;
    
    public ICollection<Recipe> Recipes { get; set; }
    
    public ICollection<Recipe> FavoriteRecipes { get; set; }
    
}