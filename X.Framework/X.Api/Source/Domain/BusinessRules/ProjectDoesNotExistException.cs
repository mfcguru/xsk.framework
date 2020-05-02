using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class ProjectDoesNotExistException : BusinessRulesException
    {
        private const string message = "The project with the specified ID does not exist";

        public ProjectDoesNotExistException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
