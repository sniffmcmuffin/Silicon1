using Microsoft.AspNetCore.Mvc;

namespace Silicon1.Controllers;

public class AuthController : Controller
{
	[Route("/signup")] // Så kan gå direkt till signup i stället för /auth/signup/
	public IActionResult SignUp()
	{
		return View();
	}
}
