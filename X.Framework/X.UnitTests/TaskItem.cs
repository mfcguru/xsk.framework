using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;
using X.Api.Source.Domain.Requests.Queries;

namespace X.UnitTests
{
	[TestClass]
	public class TaskItem
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

			var project1 = new Api.Entities.Project
			{
				TeamId = 1,
				ProjectId = 1,
				ProjectName = "My Project 1",
				Description = "My Description",
				IsActive = true,
				ImageUrl = "myurl"
			};

			var project2 = new Api.Entities.Project
			{
				TeamId = 2,
				ProjectId = 2,
				ProjectName = "My Project 2",
				Description = "My Description",
				IsActive = true,
				ImageUrl = "myurl"
			};

			var task1 = new Api.Entities.TaskItem
			{
				ProjectId = 1,
				TaskItemId = 1,
				CreatedBy = 1,
				TaskItemName = "My Task",
				Description = "My Description",
				IsActive = true
			};

			var task2 = new Api.Entities.TaskItem
			{
				ProjectId = 2,
				TaskItemId = 2,
				CreatedBy = 1,
				TaskItemName = "My Task",
				IsActive = false,
				Description = "My Description"
			};

			context.Projects.Add(project1);
			context.Projects.Add(project2);
			context.TaskItems.Add(task1);
			context.TaskItems.Add(task2);
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
		[ExpectedException(typeof(InvalidProjectIdException))]
		public async Task TaskItem_InvalidProjectId()
		{
			var projectId = 0;
			var useCase = new GetAllTaskByProjectIdQuery(projectId);
			var request = new GetAllTaskByProjectIdQuery.GetAllTaskByProjectIdQueryHandler(context, mapper);

			await request.Handle(useCase, new CancellationToken());
		}

		[TestMethod]
		[ExpectedException(typeof(ProjectIdDoesNotExistException))]
		public async Task TaskItem_ProjectIdDoesNotExist()
		{
			var projectId = 3;
			var useCase = new GetAllTaskByProjectIdQuery(projectId);
			var request = new GetAllTaskByProjectIdQuery.GetAllTaskByProjectIdQueryHandler(context, mapper);

			await request.Handle(useCase, new CancellationToken());
		}

		[TestMethod]
		[ExpectedException(typeof(NoAvailableTaskException))]
		public async Task TaskItem_NoAvailableTask()
		{
			var projectId = 2;
			var useCase = new GetAllTaskByProjectIdQuery(projectId);
			var request = new GetAllTaskByProjectIdQuery.GetAllTaskByProjectIdQueryHandler(context, mapper);

			await request.Handle(useCase, new CancellationToken());
		}
	}
}
