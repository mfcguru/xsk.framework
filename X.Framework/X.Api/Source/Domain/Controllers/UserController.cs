using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.Api.Source.Domain.UsesCases.Command;
using X.Api.Source.Domain.UsesCases.Queries;

namespace X.Api.Source.Domain.Controllers
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
			await mediator.Send(new UpdateUserCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteUserCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllUsersDto>>> GetAllUsers()
		{
			var result = await mediator.Send(new GetAllUsersQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetUserByIdDto>> GetUserById(int id)
		{
			var result = await mediator.Send(new GetUserByIdQuery(id));

			return Ok(result);
		}
	}
}
