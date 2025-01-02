using AutoMapper;
using DataAccess.Type;
using DataAccess.Users;
using Domain.Users;
using JetBrains.Annotations;
namespace DataAccess;

[UsedImplicitly]
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserEntity>();
        CreateMap<UserEntity, User>();
        CreateMap<Domain.Types.Type, TypeEntity>();
        CreateMap<TypeEntity, Domain.Types.Type>();
    }
}