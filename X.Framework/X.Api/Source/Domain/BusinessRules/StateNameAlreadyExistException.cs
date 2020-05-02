using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class StateNameAlreadyExistException : BusinessRulesException
    {
        private const string message = "State name already exist.";

        public StateNameAlreadyExistException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
