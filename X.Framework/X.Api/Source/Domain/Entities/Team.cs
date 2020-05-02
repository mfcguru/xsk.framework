using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class Team
    {
        public Team()
        {
            Projects = new HashSet<Project>();
            TeamMembers = new HashSet<TeamMember>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
