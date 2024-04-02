using idInfrastructure.Contexts;
using idInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace idInfrastructure.Services;

public class AddressService(AppDbContext context)
{
	private readonly AppDbContext _context = context;

	public async Task<AddressEntity> GetAddressAsync(string UserId)
	{
		var addressEntity = await _context.Address.FirstOrDefaultAsync(x => x.UserId == UserId);
		return addressEntity!;
	}

	public async Task<bool> CreateAddressAsync(AddressEntity entity)
	{
		_context.Address.Add(entity);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> UpdateAddressAsync(AddressEntity entity)
	{
		var existing = await _context.Address.FirstOrDefaultAsync(x => x.UserId == entity.UserId);
		if (existing != null)
		{
			_context.Entry(entity).CurrentValues.SetValues(entity);
			await _context.SaveChangesAsync();

			return true;
		}

		return false;
	}
}