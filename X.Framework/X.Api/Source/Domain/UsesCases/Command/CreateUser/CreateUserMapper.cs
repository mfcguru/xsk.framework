using AutoMapper;
using X.Api.Entities;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();
        }
    }
}
