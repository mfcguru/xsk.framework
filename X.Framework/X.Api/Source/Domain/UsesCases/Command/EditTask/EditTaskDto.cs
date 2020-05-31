using System;
using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class EditTaskDto
    {
        [Required]
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int? OwnerId { get; set; }
        public string Comment { get; set; }
    }
}
