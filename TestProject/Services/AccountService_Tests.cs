using idInfrastructure.Contexts;
using idInfrastructure.Entities;
using idInfrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace TestProject.Services;

public class AccountServiceTests
{
	[Fact]
	public void Constructor_ShouldSetDependencies()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;
		var context = new AppDbContext(options);
		var userManager = UserManagerMockFactory.CreateUserManagerMock();

		// Act
		var service = new AccountService(context, userManager);

		// Assert
		Assert.Same(context, GetPrivateFieldValue(service, "_context"));
		Assert.Same(userManager, GetPrivateFieldValue(service, "_userManager"));
	}

	private object GetPrivateFieldValue(object obj, string fieldName)
	{
		var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		return field.GetValue(obj);
	}
}