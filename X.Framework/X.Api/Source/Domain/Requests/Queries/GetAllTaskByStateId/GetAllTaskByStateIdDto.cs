using System;

namespace X.Api.Source.Domain.Requests.Queries
{
    public class GetAllTaskByStateIdDto
	{
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public Guid StateId { get; set; }
        public UserDto User { get; set; }
        public class UserDto
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string OwnerInitials { get; set; }
        }

        public GetAllTaskByStateIdDto() => User = new UserDto();
    }
}

