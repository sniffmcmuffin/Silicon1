using idInfrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace TestProject.Services
{
	public static class UserManagerMockFactory
	{
		public static UserManager<UserEntity> CreateUserManagerMock()
		{
			var storeMock = new Mock<IUserStore<UserEntity>>();
			return new UserManager<UserEntity>(storeMock.Object, null, null, null, null, null, null, null, null);
		}
	}
}