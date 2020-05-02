using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
	public class UsernameIsAlreadyRegisteredException : BusinessRulesException
	{
		private const string message = "Username is already registered";

		public UsernameIsAlreadyRegisteredException() : base(HttpStatusCode.BadRequest, message) { }
	}
}
