﻿using System.Collections.Generic;

namespace X.Api.Source.Domain.UsesCases.Command
{
	public class LoginResultDto
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Token { get; set; }
		public string ProjectName { get; set; }
		public List<int> Projects { get; set; }
		public int TeamId { get; set; }
	}
}
