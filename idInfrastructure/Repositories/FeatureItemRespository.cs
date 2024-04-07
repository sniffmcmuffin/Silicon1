using idInfrastructure.Contexts;
using idInfrastructure.Entities;

namespace idInfrastructure.Repositories;

public class FeatureItemRepository(AppDbContext context) : BaseRepository<FeatureItemEntity>(context)
{
	private readonly AppDbContext _context = context;
}
