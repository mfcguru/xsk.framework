using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class TaskLog
    {
        public TaskLog()
        {
            Attachments = new HashSet<Attachment>();
        }

        public int TaskLogId { get; set; }
        public int TaskItemId { get; set; }
        public int? UserId { get; set; }
        public string Comment { get; set; }
        public DateTime LogDate { get; set; }
        public Guid StateId { get; set; }
        public bool IsChangeStateAction { get; set; }

        public virtual State State { get; set; }
        public virtual TaskItem TaskItem { get; set; }
        public virtual Member User { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
