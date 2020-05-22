using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.Requests.Command
{
    public class ConfirmInviteDto
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string Email { get; set; }
	}
}
