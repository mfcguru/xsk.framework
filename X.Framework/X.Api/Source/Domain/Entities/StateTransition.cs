using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class StateTransition
    {
        public Guid StateId { get; set; }
        public Guid TransitionId { get; set; }
    }
}
