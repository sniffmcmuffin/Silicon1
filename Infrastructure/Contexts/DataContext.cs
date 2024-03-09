using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
	public DbSet<AddressEntity> Address { get; set; }
	public DbSet<UserEntity> User { get; set; }
}
