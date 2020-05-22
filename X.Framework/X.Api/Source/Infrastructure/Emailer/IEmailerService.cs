using System.Collections.Generic;
using System.Threading.Tasks;

namespace X.Api.Source.Infrastructure
{
	public interface IEmailerService
	{
		Task Send(int teamId, string recipient);
	}
}
