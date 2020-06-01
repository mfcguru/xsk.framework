using System;
using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class AddStateDto
    {
        [Required]
        public string StateName { get; set; }

        [Required]
        public Guid NextStateColumn { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}
