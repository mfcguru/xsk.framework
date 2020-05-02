using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class StateDoesNotExistException : BusinessRulesException
    {
        private const string message = "New State ID does not exist.";

        public StateDoesNotExistException() : base(HttpStatusCode.NotFound, message) { }
    }
}
