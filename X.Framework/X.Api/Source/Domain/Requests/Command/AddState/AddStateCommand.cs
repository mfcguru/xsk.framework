using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.Requests.Command
{
    public class AddStateCommand : IRequest
    {
        public AddStateDto Dto { get; }

        public AddStateCommand(AddStateDto dto) => this.Dto = dto;

        public class RequestHandler : IRequestHandler<AddStateCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(AddStateCommand request, CancellationToken cancellationToken)
            {
                await Validate(request.Dto);

                var entity = mapper.Map<State>(request.Dto);
                
                context.States.Add(entity);

                await AdjustOrder(entity);

                await context.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task Validate(AddStateDto dto)
            {
                if (await context.States.AnyAsync(o => o.StateName == dto.StateName))
                {
                    throw new StateNameAlreadyExistException();
                }

                var project = await context.Projects.FindAsync(dto.ProjectId);
                if (project == null)
                {
                    throw new ProjectDoesNotExistException();
                }

                var nextState = await context.States.FindAsync(dto.NextStateColumn);
                if (nextState == null)
                {
                    throw new StateDoesNotExistException();
                }
            }

            private async Task AdjustOrder(State newStateEntity)
            {
                // newStateEntity will own NextStateColumn
                // previousStateEntity is the one that currently owns NextStateColumn
                // previousStateEntity should now own newStateEntity's NextStateColumn instead
                var previousStateEntity = await context.States.SingleAsync(o => o.NextStateColumn == newStateEntity.NextStateColumn);
                previousStateEntity.NextStateColumn = newStateEntity.StateId;

                context.States.Update(previousStateEntity);
            }
        }
    }
}
