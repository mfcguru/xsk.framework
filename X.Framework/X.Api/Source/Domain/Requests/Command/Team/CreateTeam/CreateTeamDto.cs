namespace X.Api.Source.Domain.Requests.Command
{
    public class CreateTeamDto
    {
        public string TeamName { get; set; }
        public string Decription { get; set; }
        public int CompanyId { get; set; }
    }

    public class GetAllTeamsDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Decription { get; set; }
        public CompanyDto Company { get; set; }

        public class CompanyDto
        {
            public int CompanyId { get; set; }
            public string CompanyName { get; set; }
        }

    }

    public class GetTeamById
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Decription { get; set; }
        public CompanyDto Company { get; set; }

        public class CompanyDto
        {
            public int CompanyId { get; set; }
            public string CompanyName { get; set; }
        }

    }
}
