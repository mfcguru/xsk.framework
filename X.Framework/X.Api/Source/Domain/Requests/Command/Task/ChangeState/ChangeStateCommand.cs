using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.Requests.Command
{
    public class ChangeStateCommand : IRequest
    {
        public int TaskId { get; }
        public Guid NewStateId { get; }

        public ChangeStateCommand(int taskId, Guid newStateId)
        {
            TaskId = taskId;
            NewStateId = newStateId;
        }

        private class RequestHandler : IRequestHandler<ChangeStateCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(ChangeStateCommand request, CancellationToken cancellationToken)
            {
                await ValidateBusinessRules(request.TaskId, request.NewStateId);

                var lastLogEntry = await context
                    .TaskLogs
                    .Where(o => o.TaskItemId == request.TaskId)
                    .OrderByDescending(o => o.LogDate)
                    .FirstAsync();

                context.TaskLogs.Add(new TaskLog
                {
                    TaskItemId = request.TaskId,
                    LogDate = DateTime.UtcNow,
                    IsChangeStateAction = true,
                    StateId = request.NewStateId,
                    Comment = lastLogEntry.Comment,
                    UserId = lastLogEntry.UserId
                });

                await context.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task ValidateBusinessRules(int taskId, Guid newStateId)
            {
                var state = await context.States.SingleOrDefaultAsync(o => o.StateId == newStateId);
                if (state == null)
                {
                    throw new StateDoesNotExistException();
                }

                var task = await context.TaskItems.SingleOrDefaultAsync(o => o.TaskItemId == taskId);
                if (task == null)
                {
                    throw new TaskDoesNotExistException();
                }

                if (task.ProjectId != state.ProjectId)
                {
                    throw new InvalidTaskStateException();
                }
            }
        }
    }
}
