using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.Requests.Queries
{
    public class GetTaskByIdQuery : IRequest<GetTaskByIdDto>
    {
        public int Id { get; }

        public GetTaskByIdQuery(int id) => this.Id = id;

        private class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, GetTaskByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetTaskByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetTaskByIdDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
            {
                var task = await context.TaskItems
                    .Where(o => o.IsActive)
                    .SingleOrDefaultAsync(o => o.TaskItemId == request.Id);

                if (task == null)
                {
                    throw new NotFoundException();
                }

                var lastLog = await context
                    .TaskLogs
                    .OrderByDescending(o => o.LogDate)
                    .FirstAsync(o => o.TaskItemId == request.Id);

                var dto = mapper.Map<GetTaskByIdDto>(task);
                dto.Comment = lastLog.Comment;
                if (lastLog.UserId.HasValue)
                {
                    dto.Owner = new GetTaskByIdDto.OwnerDto 
                    { 
                        UserId = lastLog.UserId.Value, 
                        Username = lastLog.User.User.Username 
                    };
                }
                
                return dto;
            }
        }
    }
}
