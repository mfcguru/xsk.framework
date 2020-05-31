using AutoMapper;
using X.Api.Entities;

namespace X.Api.Source.Domain.Requests.Queries
{
	public class GetAllStatesByProjectIdMapper : Profile
	{
		public GetAllStatesByProjectIdMapper()
		{
			CreateMap<State, GetAllStatesByProjectIdDto>()
				.ForMember(dest => dest.CanUpdate, opt => opt.MapFrom(src => src.StateName == "Backlog" || src.StateName == "DONE" ? false : true))
				;
		}
	}
}
