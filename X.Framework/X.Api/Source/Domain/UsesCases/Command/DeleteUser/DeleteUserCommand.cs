using MediatR;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class DeleteUserCommand : IRequest
    {
        public int UserId { get; set; }

        public DeleteUserCommand(int userId) => this.UserId = userId;

        private class RequestHandler : IRequestHandler<DeleteUserCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Users.FindAsync(request.UserId);
                if (entity == null)
                {
                    throw new NotFoundException();
                }

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
