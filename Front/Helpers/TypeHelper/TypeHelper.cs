using AutoMapper;
using Core.IoC;
using DataAccess.Types;
using Front.Models.Type;
using JetBrains.Annotations;

namespace Front.Helpers.TypeHelper;

[UsedImplicitly]
[PutInIoC(Lifetime = ServiceLifetime.Scoped)]
public class TypeHelper : ITypeHelper
{
    private readonly ITypesRepository _typesRepository;
    private readonly IMapper _mapper;

    public TypeHelper(ITypesRepository typesRepository, IMapper mapper)
    {
        _typesRepository = typesRepository;
        _mapper = mapper;
    }
    
    public async Task<TypesResponseJsModel> GetTypes()
    {
        var serials = await _typesRepository.GetTypesAsync();
        return new(){Types= _mapper.Map<List<TypeResponseJs>>(serials)};
    }
    
}