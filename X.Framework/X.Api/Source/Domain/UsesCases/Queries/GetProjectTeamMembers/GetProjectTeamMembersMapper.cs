﻿using AutoMapper;
using System.Linq;

namespace X.Api.Source.Domain.UsesCases.Queries
{
	public class GetProjectTeamMembersMapper : Profile
	{
		public GetProjectTeamMembersMapper()
		{
			CreateMap<X.Api.Entities.TeamMember, GetProjectTeamMembersDto>()
				.ForMember(dest => dest.MemberId, opt => opt.MapFrom(o => o.User.UserId))
				.ForMember(dest => dest.MemberName, opt => opt.MapFrom(o => string.Format("{0} {1}", o.User.FirstName, o.User.LastName)))
				.ForMember(dest => dest.MemberInitials, opt => opt.MapFrom(o => string.Format("{0}{1}", o.User.FirstName.ToUpper()[0], o.User.LastName.ToUpper()[0])))
				.ForMember(dest => dest.Rgb, opt => opt.MapFrom(src => src.RgbLookup.RgbLookupValue))
				;
		}
	}
}
