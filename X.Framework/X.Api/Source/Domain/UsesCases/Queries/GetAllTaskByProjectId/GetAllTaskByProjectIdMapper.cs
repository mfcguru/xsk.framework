using AutoMapper;
using X.Api.Entities;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetAllTaskByProjectIdMapper : Profile
    {
        public GetAllTaskByProjectIdMapper()
        {
            CreateMap<TaskItem, GetAllTaskByProjectIdDto>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskItemId))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskItemName));
        }
    }
}
