using idInfrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace idInfrastructure.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Address { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<UserEntity>()
			.HasMany(u => u.Address)
			.WithOne(a => a.User)
			.HasForeignKey(a => a.UserId)
			.OnDelete(DeleteBehavior.Restrict); // Prevent removing User when removing Address
	}
}