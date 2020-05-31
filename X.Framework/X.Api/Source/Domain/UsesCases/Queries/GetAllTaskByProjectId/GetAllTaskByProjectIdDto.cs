using System;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetAllTaskByProjectIdDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string OwnerInitials { get; set; }
        public int CreatedBy { get; set; }
        public Guid StateId { get; set; }
    }
}
