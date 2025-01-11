using AutoMapper;
using Core.IoC;
using DataAccess.Base;
using Domain.Users;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Users;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
internal sealed class UserRepository(PostgresContext context, IMapper mapper)
    : BaseRepository(context, mapper), IUserRepository
{
    public async Task<IReadOnlyCollection<User>> GetUsersAsync()
    {
        var users = await Context.Users.AsNoTracking().ToListAsync();
        return Mapper.Map<User[]>(users);
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        var user = await Context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);
        return Mapper.Map<User>(user);
    }
    
    public async Task<User> GetUserRecipesAsync(string userName)
    {
        var user = await Context.Users
            .AsNoTracking()
            .Include(u=>u.Recipes)
            .SingleOrDefaultAsync(u => u.UserName == userName);
        return Mapper.Map<User>(user);
    }
    
    public async Task<User> GetUserFavoritesAsync(string userName)
    {
        var user = await Context.Users
            .AsNoTracking()
            .Include(u=>u.FavoriteRecipes)
            .SingleOrDefaultAsync(u => u.UserName == userName);
        return Mapper.Map<User>(user);
    }

    public async Task<User> GetUserByNameAsync(string userName)
    {
        var user = await Context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserName == userName);
        return Mapper.Map<User>(user);
    }

    public async Task<Guid> CreateUserAsync([NotNull] User user)
    {
        user.Id = Guid.NewGuid();
        await Context.Users.AddAsync(Mapper.Map<UserEntity>(user));
        return user.Id;
    }

    public async Task<Guid?> UpdateUserAsync([NotNull] User user)
    {
        var userEntity = await Context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);

        if (userEntity == null)
        {
            return null;
        }

        userEntity.UserName = user.UserName;
        userEntity.Password = user.Password;
        userEntity.Email = user.Email;

        return userEntity.Id;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await Context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
    }
}