using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class EmailIsAlreadyRegisteredException : BusinessRulesException
    {
        private const string message = "Email is already registered";

        public EmailIsAlreadyRegisteredException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
