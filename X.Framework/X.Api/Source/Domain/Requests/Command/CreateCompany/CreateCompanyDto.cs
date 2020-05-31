namespace X.Api.Source.Domain.Requests.Command
{
    public class CreateCompanyDto
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
    }
}
