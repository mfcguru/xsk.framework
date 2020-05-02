using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class ProjectIdDoesNotExistException : BusinessRulesException
    {
        private const string message = "Project ID does not exist.";

        public ProjectIdDoesNotExistException() : base(HttpStatusCode.NotFound, message) { }
    }
}
