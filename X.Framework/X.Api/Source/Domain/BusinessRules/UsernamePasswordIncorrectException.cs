using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class UsernamePasswordIncorrectException : BusinessRulesException
    {
        private const string message = "Username or password is incorrect.";

        public UsernamePasswordIncorrectException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
