using AutoMapper;
using System;
using X.Api.Entities;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class EditTaskMapper : Profile
    {
        public EditTaskMapper()
        {
            CreateMap<EditTaskDto, TaskItem>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<EditTaskDto, TaskLog>()
                .ForMember(dest => dest.IsChangeStateAction, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.LogDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.OwnerId))
                ;
            ;
        }
    }
}
