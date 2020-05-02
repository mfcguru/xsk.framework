using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class Member
    {
        public Member()
        {
            TaskLogs = new HashSet<TaskLog>();
            TeamMembers = new HashSet<TeamMember>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<TaskLog> TaskLogs { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
