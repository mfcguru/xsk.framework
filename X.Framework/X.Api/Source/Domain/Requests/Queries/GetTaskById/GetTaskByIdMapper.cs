using AutoMapper;
using X.Api.Entities;

namespace X.Api.Source.Domain.Requests.Queries
{
    public class GetTaskByIdMapper : Profile
    {
        public GetTaskByIdMapper()
        {
            CreateMap<TaskItem, GetTaskByIdDto>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskItemId))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskItemName))
                .ForMember(dest => dest.CreateBy, opt => opt.MapFrom(src => string.Format("{0} {1}", src.CreatedByNavigation.Member.FirstName, src.CreatedByNavigation.Member.LastName)))
                ;        
        }
    }
}
