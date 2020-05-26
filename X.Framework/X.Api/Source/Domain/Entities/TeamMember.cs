using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class TeamMember
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int? RgbLookupId { get; set; }

        public virtual RgbLookup RgbLookup { get; set; }
        public virtual Team Team { get; set; }
        public virtual Member User { get; set; }
    }
}
