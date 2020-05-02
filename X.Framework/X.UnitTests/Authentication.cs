using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Threading;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;
using X.Api.Source.Domain.Requests.Command;
using X.Api.Source.Infrastructure.Helpers;
using AsyncTask = System.Threading.Tasks.Task;

namespace X.UnitTests
{
	[TestClass]
	public class Authentication
	{
		private DataContext context = null;
		private IMapper mapper = null;

		[TestInitialize()]
		public void Initialize()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			context = new DataContext(options);

			var member = new Member 
			{
				FirstName = "Unknown",
				LastName = "Unknown",
				Email = "test1@gmail.com",
				PhotoUrl = "photoUrl"
			};

			var password = "correct password";
			byte[] passwordHash, passwordSalt;
			AuthHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

			context.Users.Add(new User { Username = "user101", Member = member, PasswordHash = passwordHash, PasswordSalt = passwordSalt });
			context.Companies.Add(new Company { CompanyName = "companyName", IsActive = true });
			context.SaveChanges();

			var applicationAssembly = Assembly.Load("X.Api");
			var config = new MapperConfiguration(configuration =>
			{
				configuration.AddMaps(applicationAssembly);
			});
			mapper = config.CreateMapper();
		}

		[TestCleanup()]
		public void Cleanup()
		{
			context.Dispose();
			context = null;
		}

		[TestMethod]
		[ExpectedException(typeof(UsernameIsAlreadyRegisteredException))]
		public async AsyncTask RegisterUser_UsernameIsAlreadyRegistered()
		{
			var dto = new RegisterUserDto();
			dto.UserInfo = new RegisterUserDto.UserInfoDto 
			{   
				FirstName = "Unknown",
				LastName = "Unknown",
				Email = "test1@gmail.com",
				Username = "user101",
				Password = "password"
			};
			dto.ProjectInfo = new RegisterUserDto.ProjectInfoDto { ProjectName = "projectName" };

			var useCase = new RegisterUserCommand(dto);
			var request = new RegisterUserCommand.RequestHandler(context, mapper);
			
			await request.Handle(useCase, new CancellationToken());
		}

		[TestMethod]
		[ExpectedException(typeof(EmailIsAlreadyRegisteredException))]
		public async AsyncTask RegisterUser_EmailIsAlreadyRegistered()
		{
			var dto = new RegisterUserDto();
			dto.UserInfo = new RegisterUserDto.UserInfoDto 
			{
				FirstName = "Unknown",
				LastName = "Unknown",
				Email = "test1@gmail.com",
				Username = "user102",
				Password = "password"
			};
			dto.ProjectInfo = new RegisterUserDto.ProjectInfoDto { ProjectName = "projectName" };

			var useCase = new RegisterUserCommand(dto);
			var request = new RegisterUserCommand.RequestHandler(context, mapper);

			await request.Handle(useCase, new CancellationToken());
		}

		[TestMethod]
		[ExpectedException(typeof(CompanyNameIsAlreadyRegisteredException))]
		public async AsyncTask RegisterUser_CompanyNameIsAlreadyRegistered()
		{
			var dto = new RegisterUserDto();
			dto.UserInfo = new RegisterUserDto.UserInfoDto
			{
				FirstName = "Unknown",
				LastName = "Unknown",
				Email = "test3@gmail.com",
				Username = "user103",
				Password = "password"
			};

			dto.CompanyInfo = new RegisterUserDto.CompanyInfoDto { CompanyName = "companyName" };
			dto.ProjectInfo = new RegisterUserDto.ProjectInfoDto { ProjectName = "projectName" };

			var useCase = new RegisterUserCommand(dto);
			var request = new RegisterUserCommand.RequestHandler(context, mapper);

			await request.Handle(useCase, new CancellationToken());
		}

		[TestMethod]
		[ExpectedException(typeof(UsernamePasswordIncorrectException))]
		public async AsyncTask Login_UsernameIsIncorrect()
		{
			var dto = new LoginDto 
			{
				Username = "wrong username",
				Password = "correct password",
			};
			
			var useCase = new LoginCommand(dto);
			var request = new LoginCommand.RequestHandler(context, mapper);

			await request.Handle(useCase, new CancellationToken());
		}

		[TestMethod]
		[ExpectedException(typeof(UsernamePasswordIncorrectException))]
		public async AsyncTask Login_PasswordIsIncorrect()
		{
			var dto = new LoginDto
			{
				Username = "user101",
				Password = "wrong password",
			};

			var useCase = new LoginCommand(dto);
			var request = new LoginCommand.RequestHandler(context, mapper);

			await request.Handle(useCase, new CancellationToken());
		}
	}
}
