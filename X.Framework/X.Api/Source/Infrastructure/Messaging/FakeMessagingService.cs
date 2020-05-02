using System.Collections.Generic;

namespace X.Api.Source.Infrastructure.Messaging
{
	public class FakeMessagingService : IMessagingService
	{
		public void Send(List<string> recipients) { }
	}
}
