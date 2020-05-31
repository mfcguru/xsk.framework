using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetProjectTeamMembersQuery : IRequest<List<GetProjectTeamMembersDto>>
    {
        public int ProjectId { get; }

        public GetProjectTeamMembersQuery(int projectId) => this.ProjectId = projectId;

        private class GetProjectTeamMembersByUserIdQueryHandler : IRequestHandler<GetProjectTeamMembersQuery, List<GetProjectTeamMembersDto>>
        {
            private DataContext context;
            private IMapper mapper;

            public GetProjectTeamMembersByUserIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetProjectTeamMembersDto>> Handle(GetProjectTeamMembersQuery request, CancellationToken cancellationToken)
            {
                var project = await context.Projects.FindAsync(request.ProjectId);
                var members = project.Team.TeamMembers.ToList();

                for (int index = 0; index < members.Count; index++)
                {
                    var member = members[index];

                    if (member.RgbLookupId == null)
                    {
                        member.RgbLookupId = (await context.RgbLookups.FindAsync(index + 1)).RgbLookupId;
                    }
                }

                await context.SaveChangesAsync();

                return mapper.Map<List<GetProjectTeamMembersDto>>(members);
            }
        }
    }
}
