using Domain.Base;

namespace Domain.Users;

public interface IUserRepository : IBaseRepository
{
    public Task<IReadOnlyCollection<User>> GetUsersAsync();
    public Task<User> GetUserByIdAsync(Guid userId);
    public Task<User> GetUserByNameAsync(String userName);
    
    public Task<Guid> CreateUserAsync(User user);
    public Task<Guid?> UpdateUserAsync(User user);
    public Task DeleteUserAsync(Guid id);
}