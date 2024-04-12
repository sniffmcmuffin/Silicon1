using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon1.ViewModels;

namespace Silicon1.Controllers;

public class CoursesController(HttpClient httpClient) : Controller
{
	private readonly HttpClient _httpClient = httpClient;

	[Route("/courses")]
	public async Task<IActionResult> Index()
	{
		var viewModel = new CourseIndexViewModel();

		var response = await _httpClient.GetAsync("https://localhost:7099/api/courses");

		if (response.IsSuccessStatusCode)
		{
			var courses = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(await response.Content.ReadAsStringAsync());
		
			if (courses != null && courses.Any()) 
				viewModel.Courses = courses;
		}

		return View(viewModel);
	}
}
