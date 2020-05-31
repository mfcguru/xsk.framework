using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.Api.Source.Domain.Requests.Queries;

namespace X.Api.Source.Domain.Requests
{
	[Route("api/[controller]")]
    [ApiController]
    public partial class ProjectController : ControllerBase
    {
		private readonly IMediator mediator;

		public ProjectController(IMediator mediator) => this.mediator = mediator;

		[HttpGet("{userId}")]
		public async Task<ActionResult<IEnumerable<GetAllTaskByProjectIdDto>>> GetAllProjectsByUserId(int userId)
		{
			var result = await mediator.Send(new GetAllProjectsByUserIdQuery(userId));
			return Ok(result);
		}

		[HttpGet("{projectId}/TeamMembers")]
		public async Task<ActionResult<IEnumerable<GetProjectTeamMembersDto>>> GetProjectTeamMembers(int projectId)
		{
			var result = await mediator.Send(new GetProjectTeamMembersQuery(projectId));
			return Ok(result);
		}
	}
}