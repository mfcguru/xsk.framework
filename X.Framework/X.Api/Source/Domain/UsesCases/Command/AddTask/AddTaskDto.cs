using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class AddTaskDto
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string TaskItemName { get; set; }
        
        public string Description { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public int? AssignedTo { get; set; }
    }
}
