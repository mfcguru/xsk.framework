using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;
using X.Api.Source.Infrastructure;
using X.Api.Source.Infrastructure.Helpers;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class InviteTeamMemberCommand : IRequest
    {
        public InviteTeamMemberDto Dto { get; }
        public int TeamId { get; }

        public InviteTeamMemberCommand(int teamId, InviteTeamMemberDto dto)
        {
            TeamId = teamId;
            Dto = dto;
        }

        private class RequestHandler : IRequestHandler<InviteTeamMemberCommand>
        {
            private readonly DataContext context;
            private readonly IEmailerService emailService;

            public RequestHandler(DataContext context, IEmailerService messagingService)
            {
                this.context = context;
                this.emailService = messagingService;
            }

            public async Task<Unit> Handle(InviteTeamMemberCommand request, CancellationToken cancellationToken)
            {
                await Validate(request.Dto);

                await emailService.Send(request.TeamId, request.Dto.Email);

                return Unit.Value;
            }

            private async Task Validate(InviteTeamMemberDto dto)
            {
                if (string.IsNullOrWhiteSpace(dto.Email))
                    throw new EmailAddressIsRequiredException();

                if (dto.Email.IsValidEmail() == false)
                    throw new InvalidEmailAddressFormatException();

                if (await context.Members.AnyAsync(m => m.Email == dto.Email))
                    throw new EmailIsAlreadyRegisteredException();
            }
        }
    }
}
