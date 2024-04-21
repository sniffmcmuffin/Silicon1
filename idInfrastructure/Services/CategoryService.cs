using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using idInfrastructure.Models;

namespace idInfrastructure.Services;

public class CategoryService(HttpClient httpClient, IConfiguration configuration)
{
	private readonly HttpClient _httpClient = httpClient;
	private readonly IConfiguration _configuration = configuration;

	public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
	{
		var response = await _httpClient.GetAsync(_configuration["ApiUris:Categories"]);

		if (response.IsSuccessStatusCode)
		{
			var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(await response.Content.ReadAsStringAsync());
			return categories ??= null!;	
		}

		return null!;
	}
}
