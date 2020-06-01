using AutoMapper;
using System;
using X.Api.Entities;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class AddStateMapper : Profile
    {
        public AddStateMapper()
        {
            CreateMap<AddStateDto, State>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => Guid.NewGuid()))
                ; 
        }
    }
}
