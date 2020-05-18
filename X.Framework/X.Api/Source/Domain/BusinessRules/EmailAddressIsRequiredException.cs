using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class EmailAddressIsRequiredException : BusinessRulesException
    {
        private const string message = "Email Address is required";

        public EmailAddressIsRequiredException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
