using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using X.Api.Source.Domain.Requests.Command.TeamMember.InviteTeamMember;

namespace X.Api.Source.Domain.Requests.Command.TeamMember
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly IMediator mediator;

        public TeamMemberController(IMediator mediator) => this.mediator = mediator;

        [HttpPost("SendTeamMemberInvitation")]
        public async Task<IActionResult> SendInvite([FromBody] InviteTeamMemberDto dto)
        {
            var result = await mediator.Send(new InviteTeamMemberCommand(dto));
            return Ok(new { message = result });
        }
    }
}
