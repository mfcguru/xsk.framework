using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace X.Api.Source.Domain.Requests.Command
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
    }
}
