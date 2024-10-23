
namespace Domain.Users;

public class User(string userName, string password, bool isAdmin)
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; } = userName;

    public string Password { get; set; } = password;
    
}