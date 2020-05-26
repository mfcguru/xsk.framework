using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class RgbLookup
    {
        public RgbLookup()
        {
            TeamMembers = new HashSet<TeamMember>();
        }

        public int RgbLookupId { get; set; }
        public string RgbLookupValue { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
