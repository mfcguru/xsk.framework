using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.Requests.Command
{
    public class ReorderStatesCommand : IRequest
    {
        public List<Guid> StateGuids { get; set; }

        public ReorderStatesCommand(List<Guid> stateGuids)
        {
            StateGuids = stateGuids;
        }

        private class RequestHandler : IRequestHandler<ReorderStatesCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(ReorderStatesCommand request, CancellationToken cancellationToken)
            {
                await Validate(request.StateGuids);

                for (int index = 0, end = request.StateGuids.Count - 1; index <= end; index++)
                {
                    var guid = request.StateGuids[index];
                    var state = await context.States.FindAsync(guid);

                    if (index == end)
                        state.NextStateColumn = null;
                    else
                        state.NextStateColumn = request.StateGuids[index + 1];

                    context.States.Update(state);
                }

                await context.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task Validate(List<Guid> stateGuids)
            {
                int? projectId = null;

                foreach (var guid in stateGuids)
                {
                    var entity = await context.States.FindAsync(guid);

                    if (projectId == null)
                    {
                        projectId = entity.ProjectId;
                        continue;
                    }

                    if (entity.ProjectId != projectId)
                    {
                        throw new OneOrMoreProjectIdsAreInvalidException();
                    }
                }
            }
        }
    }
}
