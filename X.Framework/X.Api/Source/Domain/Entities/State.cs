using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class State
    {
        public State()
        {
            InverseNextStateColumnNavigation = new HashSet<State>();
            TaskLogs = new HashSet<TaskLog>();
        }

        public Guid StateId { get; set; }
        public string StateName { get; set; }
        public Guid? NextStateColumn { get; set; }
        public int ProjectId { get; set; }
        public bool IsActive { get; set; }

        public virtual State NextStateColumnNavigation { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<State> InverseNextStateColumnNavigation { get; set; }
        public virtual ICollection<TaskLog> TaskLogs { get; set; }
    }
}
