using idInfrastructure.Contexts;
using idInfrastructure.Entities;
using idInfrastructure.Factories;
using idInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace idInfrastructure.Repositories;

public class FeatureRepository(AppDbContext context) : BaseRepository<FeatureEntity>(context)
{
	private readonly AppDbContext _context = context;

	public override async Task<ResponseResult> GetAllAsync()
	{
		try
		{
			IEnumerable<FeatureEntity> result = await _context.Features
				.Include(i => i.FeatureItems)
				.ToListAsync();

			return ResponseFactory.Ok(result);
		}

		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}
}
