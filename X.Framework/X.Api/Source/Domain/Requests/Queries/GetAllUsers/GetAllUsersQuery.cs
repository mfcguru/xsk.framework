using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;

namespace X.Api.Source.Domain.Requests.Queries
{
    public class GetAllUsersQuery : IRequest<List<GetAllUsersDto>>
    {
        private class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllUsersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllUsersDto>>(await context.Users.ToListAsync());
                return result;
            }
        }
    }
}
