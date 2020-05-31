using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetAllProjectsByUserIdQuery : IRequest<List<GetAllProjectsByUserIdDto>>
    {
        public int UserId { get; }

        public GetAllProjectsByUserIdQuery(int userId) => this.UserId = userId;

        private class GetAllProjectsByUserIdQueryHandler : IRequestHandler<GetAllProjectsByUserIdQuery, List<GetAllProjectsByUserIdDto>>
        {
            private DataContext context;
            private IMapper mapper;

            public GetAllProjectsByUserIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllProjectsByUserIdDto>> Handle(GetAllProjectsByUserIdQuery request, CancellationToken cancellationToken)
            {
                await Validate(request.UserId);

                var result = await context.Projects
                    .Where(o => o.Team.TeamMembers.Any(k => k.UserId == request.UserId))
                    .ToListAsync();

                return mapper.Map<List<GetAllProjectsByUserIdDto>>(result);
            }

            private async Task Validate(int userId)
            {
                var user = await context.Members.SingleOrDefaultAsync(o => o.UserId == userId);
                if (user == null)
                {
                    throw new UserDoesNotExistException();
                }
            }
        }
    }
}
