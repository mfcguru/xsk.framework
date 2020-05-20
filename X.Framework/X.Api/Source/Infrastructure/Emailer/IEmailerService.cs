using System.Collections.Generic;

namespace X.Api.Source.Infrastructure
{
	public interface IEmailerService
	{
		void Send(List<string> recipients);
	}
}
