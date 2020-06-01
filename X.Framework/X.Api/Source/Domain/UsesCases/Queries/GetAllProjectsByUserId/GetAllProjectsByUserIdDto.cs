namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetAllProjectsByUserIdDto
	{
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
