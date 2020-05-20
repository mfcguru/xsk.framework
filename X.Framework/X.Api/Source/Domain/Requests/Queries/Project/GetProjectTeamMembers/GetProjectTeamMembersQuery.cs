using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.Requests.Queries
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

                return mapper.Map<List<GetProjectTeamMembersDto>>(members);
            }
        }
    }
}
