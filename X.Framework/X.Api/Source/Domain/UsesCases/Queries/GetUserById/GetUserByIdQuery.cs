using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetUserByIdQuery : IRequest<GetUserByIdDto>
    {
        public int Id { get; }

        public GetUserByIdQuery(int id) => this.Id = id;

        private class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetUserByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.Users.FindAsync(request.Id);
                return mapper.Map<GetUserByIdDto>(user);
            }
        }
    }
}
