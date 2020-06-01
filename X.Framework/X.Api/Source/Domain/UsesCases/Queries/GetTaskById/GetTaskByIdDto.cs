namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetTaskByIdDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public OwnerDto Owner { get; set; }
        public string Comment { get; set; }
        public string CreateBy { get; set; }

        public class OwnerDto
        {
            public int UserId { get; set; }
            public string Username { get; set; }
        }
    }
}
