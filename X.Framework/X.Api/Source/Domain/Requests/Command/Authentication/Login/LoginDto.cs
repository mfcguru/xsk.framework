using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.Requests.Command
{
	public class LoginDto
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
