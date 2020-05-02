using AutoMapper;

namespace X.Api.Source.Domain.Requests.Queries
{
	public class GetAllProjectsByUserIdMapper : Profile
	{
		public GetAllProjectsByUserIdMapper()
		{
			CreateMap<X.Api.Entities.Project, GetAllProjectsByUserIdDto>()
				.ForMember(dest => dest.Description, opt => opt.NullSubstitute(string.Empty))
				.ForMember(dest => dest.ImageUrl, opt => opt.NullSubstitute(string.Empty))
				;
		}
	}
}
