using AutoMapper;
using Domain.Base;

namespace DataAccess.Base;

internal abstract class BaseRepository(PostgresContext context, IMapper mapper) : IBaseRepository
{
    protected readonly PostgresContext Context = context;
    protected readonly IMapper Mapper = mapper;

    public void Dispose() => Context.Dispose();
    public ValueTask DisposeAsync() => Context.DisposeAsync();

    public async Task<int> SaveChangesAsync(bool clearTracker = false)
    {
        var changes = await Context.SaveChangesAsync();
        if (clearTracker)
            Context.ChangeTracker.Clear();

        return changes;
    }
}