using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;
using X.Api.Source.Infrastructure.Helpers;

namespace X.Api.Source.Domain.Requests.Command
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserDto Dto { get; }

        public CreateUserCommand(CreateUserDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateUserCommand, int>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<User>(request.Dto);

                // validation
                if (string.IsNullOrWhiteSpace(request.Dto.Password))
                    throw new PasswordIsRequiredException();

                if (context.Users.Where(o => o.Username == request.Dto.Username).Any())
                    throw new UniqueConsraintException();

                byte[] passwordHash, passwordSalt;
                AuthHelper.CreatePasswordHash(request.Dto.Password, out passwordHash, out passwordSalt);

                entity.PasswordHash = passwordHash;
                entity.PasswordSalt = passwordSalt;

                context.Add(entity);
                await context.SaveChangesAsync();

                return entity.UserId;
            }
        }
    }
}
