using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace X.Api.Source.Domain.Requests.Queries
{
	[Route("api/[controller]")]
	[ApiController]
	public partial class StateController : ControllerBase
	{
		private readonly IMediator mediator;

		public StateController(IMediator mediator) => this.mediator = mediator;


		[HttpGet("{projectId}")]
		public async Task<IActionResult> GetAllStatesByProjectId(int projectId)
		{
			var result = await mediator.Send(new GetAllStatesByProjectIdQuery(projectId));

			return Ok(result);
		}
	}
}
