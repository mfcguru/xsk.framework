using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.Requests.Command
{
    [Authorize]
    public class AddTaskCommand : IRequest
    {
        public AddTaskDto Dto { get; }

        public AddTaskCommand(AddTaskDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<AddTaskCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(AddTaskCommand request, CancellationToken cancellationToken)
            {
                var state = await this.context.States.SingleOrDefaultAsync(o => o.ProjectId == request.Dto.ProjectId && o.StateName == "Backlog");
                if (state == null)
                    throw new InvalidProjectIdException();

                var taskLog = new TaskLog
                {
                    IsChangeStateAction = true,
                    LogDate = DateTime.UtcNow,
                    StateId = state.StateId
                };

                var taskItem = mapper.Map<TaskItem>(request.Dto);
                taskItem.TaskLogs = new HashSet<TaskLog> { taskLog };

                context.TaskItems.Add(taskItem);
                await context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
