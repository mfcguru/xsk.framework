using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class TaskItem
    {
        public TaskItem()
        {
            TaskLogs = new HashSet<TaskLog>();
        }

        public int TaskItemId { get; set; }
        public string TaskItemName { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<TaskLog> TaskLogs { get; set; }
    }
}
