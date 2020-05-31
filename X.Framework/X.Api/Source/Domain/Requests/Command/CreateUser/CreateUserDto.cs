using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.Requests.Command
{
	public class CreateUserDto
    {
		public string Username { get; set; }
		public string Password { get; set; }

		//public MemberDto Member { get; set; }

		//public class MemberDto
		//{
		//	public string FirstName { get; set; }
		//	public string LastName { get; set; }
		//	public string Email { get; set; }
		//	public string PhotoUrl { get; set; }
		//}
	}
}
