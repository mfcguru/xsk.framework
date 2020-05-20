using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.Api.Source.Infrastructure;

namespace X.Api.Source.Domain.Requests.Command
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ProjectController : ControllerBase
    {
        private IEmailerService emailerService;

        public ProjectController(IEmailerService emailerService) => this.emailerService = emailerService;
    }
}