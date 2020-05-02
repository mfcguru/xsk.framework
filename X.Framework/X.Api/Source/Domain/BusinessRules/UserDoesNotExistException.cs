using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class UserDoesNotExistException : BusinessRulesException
    {
        private const string message = "User ID does not exist.";

        public UserDoesNotExistException() : base(HttpStatusCode.NotFound, message) { }
    }
}
