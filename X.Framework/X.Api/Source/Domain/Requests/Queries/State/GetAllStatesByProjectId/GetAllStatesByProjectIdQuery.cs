using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.Requests.Queries
{
    public class GetAllStatesByProjectIdQuery : IRequest<List<GetAllStatesByProjectIdDto>>
    {
        public int ProjectId { get; }

        public GetAllStatesByProjectIdQuery(int projectId)
        {
            ProjectId = projectId;
        }

        private class GetAllStatesByProjectIdQueryHandler : IRequestHandler<GetAllStatesByProjectIdQuery, List<GetAllStatesByProjectIdDto>>
        {
            private DataContext context;
            private IMapper mapper;

            public GetAllStatesByProjectIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllStatesByProjectIdDto>> Handle(GetAllStatesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                await ValidateIfProjectDoesNotExist(request.ProjectId);

                var result = new List<GetAllStatesByProjectIdDto>();
                var currentState = await context.States.SingleAsync(o => o.ProjectId == request.ProjectId && o.StateName == "Backlog");

                while (true)
                {
                    result.Add(mapper.Map<GetAllStatesByProjectIdDto>(currentState));

                    var nextState = await context.States.SingleOrDefaultAsync(o => o.StateId == currentState.NextStateColumn);
                    if (nextState == null) break;
                    currentState = nextState;
                }

                return result;
            }

            private async Task ValidateIfProjectDoesNotExist(int projectId)
            {
                if (await context.Projects.AnyAsync(o => o.ProjectId == projectId) == false)
                {
                    throw new ProjectDoesNotExistException();
                }
            }
        }
    }
}
