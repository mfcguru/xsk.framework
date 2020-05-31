using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.Requests.Command
{
    public class DeleteTaskCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteTaskCommand(int id)
        {
            Id = id;
        }

        private class RequestHandler : IRequestHandler<DeleteTaskCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            {                
                var entity = await this.context.TaskItems
                    .Where(o => o.IsActive)
                    .SingleOrDefaultAsync(o => o.TaskItemId == request.Id);

                if (entity == null)
                {
                    throw new NotFoundException();
                }

                entity.IsActive = false;

                context.TaskItems.Update(entity);

                await context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
