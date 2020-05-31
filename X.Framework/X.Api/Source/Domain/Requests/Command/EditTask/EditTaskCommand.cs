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
    public class EditTaskCommand : IRequest
    {
        public int Id { get; set; }
        public EditTaskDto Dto { get; }

        public EditTaskCommand(int id, EditTaskDto dto)
        {
            Id = id;
            Dto = dto;
        }

        private class RequestHandler : IRequestHandler<EditTaskCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(EditTaskCommand request, CancellationToken cancellationToken)
            {                
                var task = await this.context.TaskItems.FindAsync(request.Id);
                if (task == null)
                {
                    throw new NotFoundException();
                }

                context.TaskItems.Update(mapper.Map(request.Dto, task));

                await AppendTaskLog(request.Id, request.Dto);

                await context.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task AppendTaskLog(int taskId, EditTaskDto dto)
            {
                var lastLog = await context
                    .TaskLogs
                    .OrderByDescending(o => o.LogDate)
                    .FirstAsync(o => o.TaskItemId == taskId);

                var newLog = mapper.Map<TaskLog>(dto);
                newLog.TaskItemId = taskId;
                newLog.StateId = lastLog.StateId;

                context.TaskLogs.Add(newLog);
            }
        }
    }
}
