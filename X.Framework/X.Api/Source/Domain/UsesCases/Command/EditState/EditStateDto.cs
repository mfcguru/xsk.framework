using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class EditStateDto
    {
        [Required]
        public string StateName { get; set; }
    }
}
