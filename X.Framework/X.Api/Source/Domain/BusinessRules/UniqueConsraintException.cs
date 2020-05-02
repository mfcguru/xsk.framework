using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class UniqueConsraintException : BusinessRulesException
    {
        private const string message = "Unique constraint.";

        public UniqueConsraintException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
