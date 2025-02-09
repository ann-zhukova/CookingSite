using Domain.Base;
using JetBrains.Annotations;

namespace DataAccess.Types;

public interface ITypesRepository: IBaseRepository
{
    Task<IReadOnlyCollection<Domain.Types.Type>> GetTypesAsync();
    Task<IReadOnlyCollection<Domain.Types.Type>> GetTypesAsync(ICollection<Guid> types);
    Task<Domain.Types.Type> GetTypeByIdAsync(Guid typeId);
    Task<Domain.Types.Type> GetTypeByNameAsync(string userName);
    Task<Guid> CreateTypeAsync([NotNull] Domain.Types.Type type);
    Task<Guid?> UpdateTypeAsync([NotNull] Domain.Types.Type type);
    Task DeleteTypeAsync(Guid id);
}