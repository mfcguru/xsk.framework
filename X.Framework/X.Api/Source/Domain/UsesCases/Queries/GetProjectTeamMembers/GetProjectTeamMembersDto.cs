namespace X.Api.Source.Domain.UsesCases.Queries
{
    public class GetProjectTeamMembersDto
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberInitials { get; set; }
        public string PhotoUrl { get; set; }
        public string Rgb { get; set; }
    }
}
