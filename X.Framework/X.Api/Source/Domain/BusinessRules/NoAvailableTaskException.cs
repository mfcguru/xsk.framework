using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace X.Api.Source.Domain.BusinessRules
{
    public class NoAvailableTaskException : BusinessRulesException
    {
        private const string message = "No available task.";

        public NoAvailableTaskException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
