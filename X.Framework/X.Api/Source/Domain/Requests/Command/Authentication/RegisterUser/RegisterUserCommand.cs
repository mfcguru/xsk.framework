using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;
using X.Api.Source.Infrastructure.Helpers;

using AsyncTask = System.Threading.Tasks.Task;

namespace X.Api.Source.Domain.Requests.Command
{
    public class RegisterUserCommand : IRequest
    {
        public RegisterUserDto Dto { get; }

        public RegisterUserCommand(RegisterUserDto dto) => this.Dto = dto;

        public class RequestHandler : IRequestHandler<RegisterUserCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                await UsernameIsAlreadyRegisteredValidation(request.Dto);
                await EmailIsAlreadyRegisteredValidation(request.Dto);
                await CompanyNameIsAlreadyRegisteredValidation(request.Dto);

                byte[] passwordHash, passwordSalt;
                AuthHelper.CreatePasswordHash(request.Dto.UserInfo.Password, out passwordHash, out passwordSalt);

                var stateIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

                var project = new Project
                {
                    ProjectName = request.Dto.ProjectInfo.ProjectName,
                    States = new HashSet<State>()
                    {
                        new State { StateId = stateIds[0], StateName = "Backlog", NextStateColumn = stateIds[1],  IsActive = true },
                        new State { StateId = stateIds[1], StateName = "In-Progress", NextStateColumn = stateIds[2],  IsActive = true },
                        new State { StateId = stateIds[2], StateName = "DONE", NextStateColumn = null,  IsActive = true },
                    },
                    IsActive = true
                };

                var team = new Team
                {
                    TeamName = string.Format("{0} Team", request.Dto.ProjectInfo.ProjectName),
                    Projects = new HashSet<Project>() { project },
                    Company = new Company
                    {
                        CompanyName = request.Dto.CompanyInfo.CompanyName,
                        Description = request.Dto.CompanyInfo.Description,
                        LogoUrl = request.Dto.CompanyInfo.LogoUrl,
                        IsActive = true
                    },
                    IsActive = true
                };

                var user = new User
                {
                    Username = request.Dto.UserInfo.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Member = new Member
                    {
                        Email = request.Dto.UserInfo.Email,
                        FirstName = request.Dto.UserInfo.FirstName,
                        LastName = request.Dto.UserInfo.LastName,
                        PhotoUrl = request.Dto.UserInfo.PhotoUrl,
                        TeamMembers = new HashSet<Entities.TeamMember> { new Entities.TeamMember { Team = team } }
                    }
                };

                context.Users.Add(user);

                await context.SaveChangesAsync();

                return Unit.Value;
            }

            private async AsyncTask UsernameIsAlreadyRegisteredValidation(RegisterUserDto dto)
            {
                if (await context.Users.AnyAsync(o => o.Username == dto.UserInfo.Username))
                {
                    throw new UsernameIsAlreadyRegisteredException();
                }
            }

            private async AsyncTask EmailIsAlreadyRegisteredValidation(RegisterUserDto dto)
            {
                if (await context.Members.AnyAsync(o => o.Email == dto.UserInfo.Email))
                {
                    throw new EmailIsAlreadyRegisteredException();
                }
            }

            private async AsyncTask CompanyNameIsAlreadyRegisteredValidation(RegisterUserDto dto)
            {
                if (await context.Companies.AnyAsync(o => o.CompanyName == dto.CompanyInfo.CompanyName))
                {
                    throw new CompanyNameIsAlreadyRegisteredException();
                }
            }
        }
    }
}
