using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.Requests.Queries
{
    public class GetAllTaskByProjectIdQuery : IRequest<List<GetAllTaskByProjectIdDto>>
    {
        public int ProjectId { get; }

        public GetAllTaskByProjectIdQuery(int projectId) => this.ProjectId = projectId;

        public class GetAllTaskByProjectIdQueryHandler : IRequestHandler<GetAllTaskByProjectIdQuery, List<GetAllTaskByProjectIdDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllTaskByProjectIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllTaskByProjectIdDto>> Handle(GetAllTaskByProjectIdQuery request, CancellationToken cancellationToken)
            {
                if (request.ProjectId <= 0)
                    throw new InvalidProjectIdException();

                var project = await context.Projects.SingleOrDefaultAsync(o => o.ProjectId == request.ProjectId);
                if (project == null)
                    throw new ProjectIdDoesNotExistException();

                var taskItems = await context.TaskItems.Where(o => o.ProjectId == request.ProjectId).ToListAsync();
                if (taskItems.Count() == 0)
                    throw new NoAvailableTaskException();

                if(!taskItems.Any(o => o.IsActive))
                    throw new NoAvailableTaskException();

                var result = mapper.Map<List<GetAllTaskByProjectIdDto>>(taskItems);

                foreach (var item in result)
                {
                    var logContext = await context.TaskLogs
                        .Where(o => o.TaskItem.ProjectId == request.ProjectId)
                        .OrderByDescending(o => o.LogDate)
                        .FirstAsync();

                    if (logContext.User != null)
                    {
                        item.StateId = logContext.StateId;
                        item.OwnerInitials = string.Format("{0}{1}", logContext.User.FirstName.ToUpper().First(), logContext.User.LastName.ToUpper().First());
                    }
                }

                return result;
            }
        }
    }
}
