using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class User
    {
        public User()
        {
            TaskItems = new HashSet<TaskItem>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
