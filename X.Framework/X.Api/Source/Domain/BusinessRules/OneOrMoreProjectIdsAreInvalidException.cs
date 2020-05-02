using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class OneOrMoreProjectIdsAreInvalidException : BusinessRulesException
    {
        private const string message = "One or more project IDs are invalid.";

        public OneOrMoreProjectIdsAreInvalidException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
