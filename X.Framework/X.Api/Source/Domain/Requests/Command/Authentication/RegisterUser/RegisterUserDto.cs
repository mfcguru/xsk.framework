using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.Requests.Command
{
	public class RegisterUserDto
	{
		public UserInfoDto UserInfo { get; set; }
		public CompanyInfoDto CompanyInfo { get; set; }
		public ProjectInfoDto ProjectInfo { get; set; }

		public class UserInfoDto
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

			public string PhotoUrl { get; set; }
		}

		public class CompanyInfoDto
		{
			[Required]
			public string CompanyName { get; set; }
			public string Description { get; set; }
			public string LogoUrl  { get; set; }
		}

		public class ProjectInfoDto
		{
			[Required]
			public string ProjectName { get; set; }
			public string Description { get; set; }
		}
	}
}
