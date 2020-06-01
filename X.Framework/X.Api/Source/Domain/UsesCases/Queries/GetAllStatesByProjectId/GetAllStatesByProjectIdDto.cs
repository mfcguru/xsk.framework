using System;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetAllStatesByProjectIdDto
	{
        public Guid StateId { get; set; }
        public string StateName { get; set; }
        public bool CanUpdate { get; set; }
    }
}
