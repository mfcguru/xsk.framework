using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;
using X.Api.Source.Infrastructure.Helpers;

namespace X.Api.Source.Domain.Requests.Command
{
    public class LoginCommand : IRequest<LoginResultDto>
    {
        public LoginDto Dto { get; set; }

        public LoginCommand(LoginDto dto) => this.Dto = dto;

        public class RequestHandler : IRequestHandler<LoginCommand, LoginResultDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await context.Users
                    .Include(o => o.Member)
                    .ThenInclude(o => o.TeamMembers)
                    .ThenInclude(o => o.Team)
                    .ThenInclude(o => o.Projects)
                    .SingleOrDefaultAsync(o => o.Username == request.Dto.Username);

                if (user == null)
                    throw new UsernamePasswordIncorrectException();

                // check if password is correct
                if (!AuthHelper.VerifyPasswordHash(request.Dto.Password, user.PasswordHash, user.PasswordSalt))
                    throw new UsernamePasswordIncorrectException();

                var projects = user.Member
                    .TeamMembers.Select(o => o.Team)
                    .SelectMany(o => o.Projects)
                    .Select(o => o.ProjectId);

                // authentication is successful
                LoginResultDto result = new LoginResultDto
                {
                    Id = user.UserId,
                    Username = user.Username,
                    FirstName = user.Member.FirstName,
                    LastName = user.Member.LastName,
                    Token = GenerateJwtToken(user),
                    Projects = projects.ToList(),
                    TeamId = user.Member.TeamMembers.First().TeamId
                };

                return result;
            }

            private string GenerateJwtToken(User user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AuthHelper.SECRET);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Username.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }
        }
    }
}
