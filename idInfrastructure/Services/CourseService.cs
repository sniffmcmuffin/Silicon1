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

		public async Task<IEnumerable<CourseModel>> GetCoursesAsync()
		{
			var response = await _httpClient.GetAsync(_configuration["ApiUris:Courses"]);

			if (response.IsSuccessStatusCode)
			{
				//var jsonResponse = await response.Content.ReadAsStringAsync();
				//var jsonObject = JObject.Parse(jsonResponse);
				//var courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(jsonObject["courses"].ToString());
				//return courses ??= null!;

				var result = JsonConvert.DeserializeObject<CourseResult>(await response.Content.ReadAsStringAsync());

				if (result != null && result.Succeeded)
					return result.Courses ??= null!;
			}

			return null!;
		}

	}
}
