namespace X.Api
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string ConnectionString { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool StmpEnableSsl { get; set; }
        public string RegistrationPageUrl { get; set; }
        public string ContactSupportEmail { get; set; }
        public string DomainName { get; set; }
        public string FacebookProfileUrl { get; set; }
        public string TwitterProfileUrl { get; set; }
        public string InstagramProfileUrl { get; set; }
        public string GithubProfileUrl { get; set; }
    }
}
