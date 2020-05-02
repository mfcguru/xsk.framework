using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class Company
    {
        public Company()
        {
            Teams = new HashSet<Team>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
