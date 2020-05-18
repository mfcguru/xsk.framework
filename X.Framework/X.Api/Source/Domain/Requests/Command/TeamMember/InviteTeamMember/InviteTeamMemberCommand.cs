using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;
using X.Api.Source.Infrastructure;
using X.Api.Source.Infrastructure.Helpers;

namespace X.Api.Source.Domain.Requests.Command.TeamMember.InviteTeamMember
{
    public class InviteTeamMemberCommand : IRequest<string>
    {
        public InviteTeamMemberDto Dto { get; }

        public InviteTeamMemberCommand(InviteTeamMemberDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<InviteTeamMemberCommand, string>
        {
            private readonly DataContext context;
            private readonly IMessagingService messagingService;

            public RequestHandler(DataContext context, IMessagingService messagingService)
            {
                this.context = context;
                this.messagingService = messagingService;
            }

            public async Task<string> Handle(InviteTeamMemberCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.Dto.Email))
                    throw new EmailAddressIsRequiredException();

                if (request.Dto.Email.IsValidEmail() == false)
                    throw new InvalidEmailAddressFormatException();

                if (await context.Members.AnyAsync(m => m.Email == request.Dto.Email))
                    throw new EmailIsAlreadyRegisteredException();

                messagingService.SendEmailInvitation(request.Dto.Email);

                return "Invitation Sent";
            }
        }
    }
}
