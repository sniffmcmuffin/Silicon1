using idInfrastructure.Contexts;
using idInfrastructure.Entities;
using idInfrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject.Services;

public class AddressServiceTests
{
	[Fact]
	public async Task CreateAddressAsync_ShouldCreateNewAddress()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase")
			.Options;

		var userId = "user1"; 
		var newAddress = new AddressEntity
		{
			UserId = userId,
			AdressLine_1 = "New Adress Line 1",
			City = "New City",
			PostalCode = "54321"				
		};

		// Act
		using (var context = new AppDbContext(options))
		{
			var service = new AddressService(context);
			var result = await service.CreateAddressAsync(newAddress);

			// Assert
			Assert.True(result); 
			
			var createdAddress = await context.Address.FirstOrDefaultAsync(a => a.UserId == userId);
			Assert.NotNull(createdAddress); 
			Assert.Equal(newAddress.AdressLine_1, createdAddress.AdressLine_1); 
			Assert.Equal(newAddress.City, createdAddress.City); 
			Assert.Equal(newAddress.PostalCode, createdAddress.PostalCode); 
		}
	}

	[Fact]
	public async Task GetAddressAsync_WithValidUserId_ShouldReturnAddress()
	{
		// Arrange
	
		// Act
	
		// Assert			
	}

	[Fact]
	public async Task UpdateAddressAsync_WithExistingAddress_ShouldUpdateAddress()
	{
		// Arrange
		
		// Act

		// Assert
	}
}