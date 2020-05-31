using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;
using X.Api.Source.Infrastructure.Helpers;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class ConfirmInviteCommand : IRequest
    {
        public ConfirmInviteDto Dto { get; }
        public int TeamId { get; }

        public ConfirmInviteCommand(int teamId, ConfirmInviteDto dto)
        {
            TeamId = teamId;
            Dto = dto;
        }

        private class RequestHandler : IRequestHandler<ConfirmInviteCommand>
        {
            private readonly DataContext context;

            public RequestHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(ConfirmInviteCommand request, CancellationToken cancellationToken)
            {
                await UsernameIsAlreadyRegisteredValidation(request.Dto);
                await EmailIsAlreadyRegisteredValidation(request.Dto);

                byte[] passwordHash, passwordSalt;
                AuthHelper.CreatePasswordHash(request.Dto.Password, out passwordHash, out passwordSalt);

                var user = new User
                {
                    Username = request.Dto.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Member = new Member
                    {
                        Email = request.Dto.Email,
                        FirstName = request.Dto.FirstName,
                        LastName = request.Dto.LastName
                    }
                };

                context.Users.Add(user);
                context.TeamMembers.Add(new TeamMember { User = user.Member, TeamId = request.TeamId });

                await context.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task UsernameIsAlreadyRegisteredValidation(ConfirmInviteDto dto)
            {
                if (await context.Users.AnyAsync(o => o.Username == dto.Username))
                {
                    throw new UsernameIsAlreadyRegisteredException();
                }
            }

            private async Task EmailIsAlreadyRegisteredValidation(ConfirmInviteDto dto)
            {
                if (await context.Members.AnyAsync(o => o.Email == dto.Email))
                {
                    throw new EmailIsAlreadyRegisteredException();
                }
            }
        }
    }
}
