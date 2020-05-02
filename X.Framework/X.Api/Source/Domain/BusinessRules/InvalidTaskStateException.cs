using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class InvalidTaskStateException : BusinessRulesException
    {
        private const string message = "The given state ID do not belong to the same project of the given task ID";

        public InvalidTaskStateException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
