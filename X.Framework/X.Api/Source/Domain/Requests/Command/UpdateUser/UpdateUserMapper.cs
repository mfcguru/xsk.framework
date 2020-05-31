using AutoMapper;
using X.Api.Entities;

namespace X.Api.Source.Domain.Requests.Command
{
    public class UpdateUserMapper : Profile
    {
        public UpdateUserMapper()
        {
            CreateMap<User, UpdateUserDto>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
