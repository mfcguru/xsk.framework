using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace X.Api.Source.Domain.Requests.Queries.Project
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
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