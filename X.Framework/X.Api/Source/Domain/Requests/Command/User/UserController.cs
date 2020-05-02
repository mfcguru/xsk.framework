using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace X.Api.Source.Domain.Requests.Command
{
	[Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
		{
			var result = await mediator.Send(new CreateUserCommand(dto));
			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto dto)
		{
			var result = await mediator.Send(new UpdateUserCommand(id, dto));
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteUserCommand(id));
			return Ok();
		}
	}
}
