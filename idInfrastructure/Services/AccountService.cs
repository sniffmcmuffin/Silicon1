using idInfrastructure.Contexts;
using idInfrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace idInfrastructure.Services;

public class AccountService
{
	private readonly AppDbContext _context;
	private readonly UserManager<UserEntity> _userManager;

	public AccountService(AppDbContext context, UserManager<UserEntity> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	//public async Task<bool>UpdateUserAsync(UserEntity user)
	//{
	//	_context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
		
	//	// UserManager nedan gör samma som Context ovan.
	//	// _userManager.Users.FirstOrDefaultAsync(user => user.Email == user.Email);
	//}
}
