using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class CompanyNameIsAlreadyRegisteredException : BusinessRulesException
    {
        private const string message = "Company name is already registered";

        public CompanyNameIsAlreadyRegisteredException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
