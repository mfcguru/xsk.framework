using AutoMapper;
using X.Api.Entities;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class AddTaskMapper : Profile
    {
        public AddTaskMapper()
        {
            CreateMap<AddTaskDto, TaskItem>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
        }
    }
}
