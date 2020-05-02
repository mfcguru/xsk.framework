using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace X.Api.Source.Domain.Requests.Queries
{
	[Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
		private readonly IMediator mediator;

		public TaskController(IMediator mediator) => this.mediator = mediator;

		[HttpGet("{projectId}/ByProjectId")]
		public async Task<ActionResult<IEnumerable<GetAllTaskByProjectIdDto>>> GetAllTaskByProjectId(int projectId)
		{
			var result = await mediator.Send(new GetAllTaskByProjectIdQuery(projectId));
			return Ok(result);
		}

		[HttpGet("{stateId}/ByStateId")]
		public async Task<ActionResult<IEnumerable<GetAllTaskByProjectIdDto>>> GetAllTasksByStateId(Guid stateId)
		{
			var result = await mediator.Send(new GetAllTaskByStateIdQuery(stateId));
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetTaskByIdDto>> GetTaskById(int id)
		{
			var result = await mediator.Send(new GetTaskByIdQuery(id));
			return Ok(result);
		}
	}
}