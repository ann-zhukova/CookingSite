namespace Domain.Base;

public interface IBaseRepository : IDisposable, IAsyncDisposable
{
    Task<int> SaveChangesAsync(bool clearTracker = false);
}