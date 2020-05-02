using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.Requests.Command
{
    public class EditStateDto
    {
        [Required]
        public string StateName { get; set; }
    }
}
