using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class TeamMember
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public string Rgb { get; set; }

        public virtual Team Team { get; set; }
        public virtual Member User { get; set; }
    }
}
