using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class Project
    {
        public Project()
        {
            States = new HashSet<State>();
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int TeamId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

        public virtual Team Team { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
