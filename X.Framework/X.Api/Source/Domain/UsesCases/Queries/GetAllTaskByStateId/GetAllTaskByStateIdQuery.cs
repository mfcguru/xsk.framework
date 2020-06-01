using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetAllTaskByStateIdQuery : IRequest<List<GetAllTaskByStateIdDto>>
    {
        public Guid StateId { get; }

        public GetAllTaskByStateIdQuery(Guid stateId) => this.StateId = stateId;

        public class GetAllTaskByStateIdQueryHandler : IRequestHandler<GetAllTaskByStateIdQuery, List<GetAllTaskByStateIdDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllTaskByStateIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllTaskByStateIdDto>> Handle(GetAllTaskByStateIdQuery request, CancellationToken cancellationToken)
            {
                var taskLogs = context.TaskLogs
                    //.Include(o => o.TaskItem)
                    //.Include(o => o.User != null ? o.User : new Member())
                    //.ThenInclude(o => o.User != null ? o.User : new User())
                    .Where(o => o.TaskItem.IsActive)
                    .Where(o => o.IsChangeStateAction)
                    // https://stackoverflow.com/questions/59346353/problem-with-ef-orderby-after-migration-to-net-core-3-1
                    .AsEnumerable()
                    .GroupBy(o => o.TaskItemId)
                    .Select(g => g.OrderByDescending(o => o.LogDate).First())
                    .Where(o => o.StateId == request.StateId)
                    .ToList()
                    ;

                var result = mapper.Map<List<GetAllTaskByStateIdDto>>(taskLogs);

                foreach (var item in result)
                {
                    item.StateId = context.TaskLogs.OrderByDescending(o => o.LogDate).First().StateId;
                }

                return result;
            }
        }
    }
}
