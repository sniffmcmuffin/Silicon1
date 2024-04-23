using idInfrastructure.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace idInfrastructure.Services
{
	public class CourseService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public CourseService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_configuration = configuration;
		}

		public async Task<IEnumerable<CourseModel>> GetCoursesAsync(string category = "", string searchQuery = "")
		{
			var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Courses"]}?category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}");

			if (response.IsSuccessStatusCode)
			{
				var result = JsonConvert.DeserializeObject<CourseResult>(await response.Content.ReadAsStringAsync());

				if (result != null && result.Succeeded)
					return result.Courses ??= null!;
			}

			return null!;
		}

	}
}
