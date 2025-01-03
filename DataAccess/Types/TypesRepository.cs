using AutoMapper;
using Core.IoC;
using DataAccess.Base;
using DataAccess.Type;
using Domain.Users;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Types;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
internal sealed class TypesRepository(PostgresContext context, IMapper mapper)
    : BaseRepository(context, mapper), ITypesRepository
{
    public async Task<IReadOnlyCollection<Domain.Types.Type>> GetTypesAsync()
    {
        var users = await Context.Types.AsNoTracking().ToListAsync();
        return Mapper.Map<Domain.Types.Type[]>(users);
    }

    public async Task<Domain.Types.Type> GetTypeByIdAsync(Guid typeId)
    {
        var user = await Context.Types.AsNoTracking().SingleOrDefaultAsync(u => u.Id == typeId);
        return Mapper.Map<Domain.Types.Type>(user);
    }

    public async Task<Domain.Types.Type> GetTypeByNameAsync(string userName)
    {
        var user = await Context.Types.AsNoTracking().SingleOrDefaultAsync(u => u.TypeName == userName);
        return Mapper.Map<Domain.Types.Type>(user);
    }

    public async Task<Guid> CreateTypeAsync([NotNull] Domain.Types.Type type)
    {
        type.Id = Guid.NewGuid();
        await Context.Types.AddAsync(Mapper.Map<TypeEntity>(type));
        return type.Id;
    }

    public async Task<Guid?> UpdateTypeAsync([NotNull] Domain.Types.Type type)
    {
        var userEntity = await Context.Types.SingleOrDefaultAsync(u => u.Id == type.Id);

        if (userEntity == null)
        {
            return null;
        }

        userEntity.TypeName = type.TypeName;
        return userEntity.Id;
    }

    public async Task DeleteTypeAsync(Guid id)
    {
        await Context.Types.Where(u => u.Id == id).ExecuteDeleteAsync();
    }
}