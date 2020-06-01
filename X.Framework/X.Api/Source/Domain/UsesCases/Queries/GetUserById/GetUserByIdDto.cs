using System.ComponentModel.DataAnnotations;

namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetUserByIdDto
    {
		public int UserId { get; set; }
		public string Username { get; set; }
		public string Initials { get; set; }
		public string Email { get; set; }
		public string PhotoUrl { get; set; }
	}
}
