using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
    public class TaskDoesNotExistException : BusinessRulesException
    {
        private const string message = "Task ID does not exist.";

        public TaskDoesNotExistException() : base(HttpStatusCode.NotFound, message) { }
    }
}
