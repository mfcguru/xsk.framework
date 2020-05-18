using System.Collections.Generic;

namespace X.Api.Source.Infrastructure.Messaging
{
	public class FakeMessagingService : IMessagingService
	{
		public void Send(List<string> recipients)
		{
			throw new System.NotImplementedException();
		}

		public void SendEmailInvitation(string receiverEmail)
		{
			throw new System.NotImplementedException();
		}
	}
}
