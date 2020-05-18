using System.Collections.Generic;

namespace X.Api.Source.Infrastructure
{
	public interface IMessagingService
	{
		void Send(List<string> recipients);
        void SendEmailInvitation(string receiverEmail);
    }
}
