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
    public class StateController : ControllerBase
	{
        private readonly IMediator mediator;

        public StateController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddState([FromBody] AddStateDto dto)
        {
            await mediator.Send(new AddStateCommand(dto));

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditState(Guid id, [FromBody] EditStateDto dto)
        {
            await mediator.Send(new EditStateCommand(id, dto));

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(Guid id)
        {
            await mediator.Send(new DeleteStateCommand(id));

            return Ok();
        }

        [HttpPatch("Reorder")]
        public async Task<IActionResult> ReorderStates([FromBody] List<Guid> stateGuids)
        {
            await mediator.Send(new ReorderStatesCommand(stateGuids));

            return Ok();
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetAllStatesByProjectId(int projectId)
        {
            var result = await mediator.Send(new GetAllStatesByProjectIdQuery(projectId));

            return Ok(result);
        }
    }
}
