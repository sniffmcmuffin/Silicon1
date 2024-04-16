using idInfrastructure.Contexts;
using idInfrastructure.Entities;
using idInfrastructure.Repositories;
using idInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestProject.Repositories
{
	public class BaseRepositoryTests
	{
		[Fact]
		public async Task CreateOneAsync_ShouldCreateEntity()
		{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase")
			.Options;
			
		using (var context = new AppDbContext(options))
		{				
			var repository = new TestRepository(context);
			var entity = new UserEntity
			{					
				Id = "1",
				UserName = "testuser",
				Email = "testuser@example.com",
				FirstName = "John",
				LastName = "Doe",
				ProfileImage = "profile.jpg",
				Bio = "This is a test bio.",
				IsExternalAccount = false,
				Address = new List<AddressEntity>
				{
				new AddressEntity
				{
					Id = 1,
					UserId = "1",
					AdressLine_1 = "123 Main St",
					City = "City",
					PostalCode = "12345"
				}
			}
		};

		// Act
		var response = await repository.CreateOneAsync(entity);

		// Assert
		Assert.True(response.StatusCode == StatusCode.OK);
		}
	}
}

	public class TestRepository : BaseRepository<UserEntity>
	{	
		public TestRepository(AppDbContext context) : base(context)
		{
		}
	}
}