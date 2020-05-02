using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace X.Api.Source.Domain.Requests.Command
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
	}
}
