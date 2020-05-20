using System.Collections.Generic;

namespace X.Api.Source.Infrastructure.Messaging
{
	public class FakeEmailerService : IEmailerService
	{
		public void Send(List<string> recipients) { }
	}
}
