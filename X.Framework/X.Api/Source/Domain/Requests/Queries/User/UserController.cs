using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace X.Api.Source.Domain.Requests.Queries
{
	[Route("api/[controller]")]
	[ApiController]
	public partial class UserController : ControllerBase
	{
		private readonly IMediator mediator;

		public UserController(IMediator mediator) => this.mediator = mediator;

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