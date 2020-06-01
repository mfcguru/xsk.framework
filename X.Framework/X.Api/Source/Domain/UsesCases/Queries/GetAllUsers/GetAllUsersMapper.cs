using AutoMapper;
using System.Linq;
using X.Api.Entities;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetAllUsersMapper : Profile
    {
        public GetAllUsersMapper()
        {
            CreateMap<User, GetAllUsersDto>()
                .ForMember(dest => dest.Initials, opt => opt.MapFrom(src => string.Format("{0}{1}", src.Member.FirstName.ToUpper().First(), src.Member.LastName.ToUpper().First()) ))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Member.Email))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Member.PhotoUrl))
                ;
        }
    }
}
