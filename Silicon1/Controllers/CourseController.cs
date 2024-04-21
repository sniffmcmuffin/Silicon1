using idInfrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Silicon1.ViewModels;


namespace Silicon1.Controllers;

public class CoursesController(CategoryService categoryService, CourseService courseService) : Controller
{
	private readonly CategoryService _categoryService = categoryService;
	private readonly CourseService _courseService = courseService;

	[Route("/courses")]
	public async Task<IActionResult> Index()
	{
		var viewModel = new CourseIndexViewModel
		{
			Categories = await _categoryService.GetCategoriesAsync(),
			Courses = await _courseService.GetCoursesAsync(),
		};

		return View(viewModel);
	}
}
