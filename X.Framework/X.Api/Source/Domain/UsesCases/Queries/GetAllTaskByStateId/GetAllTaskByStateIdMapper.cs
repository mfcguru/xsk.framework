using AutoMapper;
using System.Linq;
using X.Api.Entities;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetAllTaskByStateIdMapper : Profile
    {
        public GetAllTaskByStateIdMapper()
        {
            CreateMap<TaskLog, GetAllTaskByStateIdDto>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskItemId))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskItem.TaskItemName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TaskItem.Description))
                .ForPath(dest => dest.User.UserId, opt => opt.MapFrom(src => src.User == null ? 0 : src.User.UserId))
                .ForPath(dest => dest.User.UserName, opt => opt.MapFrom(src => src.User == null ? string.Empty : src.User.User.Username))
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.User == null ? string.Empty : src.User.User.Member.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.User == null ? string.Empty : src.User.User.Member.LastName))
                .ForPath(dest => dest.User.OwnerInitials, opt => opt.MapFrom(src => src.User == null ? string.Empty : 
                    string.Format("{0}{1}", src.User.User.Member.FirstName.ToUpper().First(), src.User.User.Member.LastName.ToUpper().First())))
                ;
        }
    }
}
