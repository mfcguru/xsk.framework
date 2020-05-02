using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace X.Api.Source.Domain.Requests.Command
{
	[Route("api/[controller]")]
	[ApiController]
	public partial class AuthenticationController : ControllerBase
	{
		private readonly IMediator mediator;

		public AuthenticationController(IMediator mediator) => this.mediator = mediator;

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var loginResult = await mediator.Send(new LoginCommand(dto));

			return Ok(loginResult);
		}

		[HttpPost("Register")]
		public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
		{
			await mediator.Send(new RegisterUserCommand(dto));

			return Ok();
		}
	}
}
