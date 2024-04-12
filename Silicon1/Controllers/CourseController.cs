using Microsoft.AspNetCore.Mvc;

namespace Silicon1.Controllers;

public class CourseController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
