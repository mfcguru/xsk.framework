using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using X.Api.Source.Domain.UsesCases.Command;

namespace X.Api.Source.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private IMediator mediator;

        public TeamController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("{teamId}/InviteTeamMember")]
        public async Task<ActionResult> InviteTeamMember(int teamId, [FromBody] InviteTeamMemberDto dto)
        {
            await mediator.Send(new InviteTeamMemberCommand(teamId, dto));

            return Ok();
        }

        [HttpPost("{teamId}/ConfirmInvite")]
        public async Task<ActionResult> ConfirmInvite(int teamId, [FromBody] ConfirmInviteDto dto)
        {
            await mediator.Send(new ConfirmInviteCommand(teamId, dto));

            return Ok();
        }
    }
}
