using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class DeleteStateCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteStateCommand(Guid id)
        {
            Id = id;
        }

        private class RequestHandler : IRequestHandler<DeleteStateCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
            {                
                var entity = await this.context.States
                    .Where(o => o.IsActive)
                    .SingleOrDefaultAsync(o => o.StateId == request.Id);

                if (entity == null)
                {
                    throw new NotFoundException();
                }

                entity.IsActive = false;

                context.States.Update(entity);

                await AdjustOrder(entity);

                await context.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task AdjustOrder(State deletedStateEntity)
            {
                // deletedStateEntity will relinquish its NextStateColumn
                // orphanedStateEntity will receive relinquished NextStateColumn
                var orphanedStateEntity = await context.States.SingleAsync(o => o.NextStateColumn == deletedStateEntity.NextStateColumn);
                orphanedStateEntity.NextStateColumn = deletedStateEntity.NextStateColumn;
                deletedStateEntity.NextStateColumn = null;

                context.States.Update(orphanedStateEntity);
            }
        }
    }
}
