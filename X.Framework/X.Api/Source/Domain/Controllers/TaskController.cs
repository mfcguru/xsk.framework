using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.Api.Source.Domain.UsesCases.Command;
using X.Api.Source.Domain.UsesCases.Queries;

namespace X.Api.Source.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TaskController : ControllerBase
    {
        private readonly IMediator mediator;

        public TaskController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDto dto)
        {
            await mediator.Send(new AddTaskCommand(dto));

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, [FromBody] EditTaskDto dto)
        {
            await mediator.Send(new EditTaskCommand(id, dto));

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await mediator.Send(new DeleteTaskCommand(id));

            return Ok();
        }

        [HttpPatch("{taskId}/ChangeState/{newStateId}")]
		public async Task<ActionResult> ChangeState(int taskId, Guid newStateId)
		{
			await mediator.Send(new ChangeStateCommand(taskId, newStateId));

			return Ok();
		}

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
