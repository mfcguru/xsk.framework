using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class InvalidProjectIdException : BusinessRulesException
    {
        private const string message = "Invalid project id.";

        public InvalidProjectIdException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
