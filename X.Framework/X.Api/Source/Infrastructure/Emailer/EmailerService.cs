using System.Threading.Tasks;

namespace X.Api.Source.Infrastructure
{
	public class EmailerService : IEmailerService
	{
		public async Task Send(int teamId, string recipient)
        {
			await Task.Run(() => { });
		}
    }
}
