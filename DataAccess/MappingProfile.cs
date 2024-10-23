using AutoMapper;
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

    }
}