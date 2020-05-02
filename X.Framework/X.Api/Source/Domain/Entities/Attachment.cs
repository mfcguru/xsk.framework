using System;
using System.Collections.Generic;

namespace X.Api.Entities
{
    public partial class Attachment
    {
        public int AttachmentId { get; set; }
        public string AttachmentUrl { get; set; }
        public int? TaskLogId { get; set; }
        public string AttachmentName { get; set; }

        public virtual TaskLog TaskLog { get; set; }
    }
}
