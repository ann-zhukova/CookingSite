using JetBrains.Annotations;

namespace DataAccess.Types;

public interface ITypesRepository
{
    Task<IReadOnlyCollection<Domain.Types.Type>> GetTypesAsync();
    Task<Domain.Types.Type> GetTypeByIdAsync(Guid typeId);
    Task<Domain.Types.Type> GetTypeByNameAsync(string userName);
    Task<Guid> CreateTypeAsync([NotNull] Domain.Types.Type type);
    Task<Guid?> UpdateTypeAsync([NotNull] Domain.Types.Type type);
    Task DeleteTypeAsync(Guid id);
}