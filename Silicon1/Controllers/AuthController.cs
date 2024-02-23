using Microsoft.AspNetCore.Mvc;
using Silicon1.Models;
using Silicon1.ViewModels;

namespace Silicon1.Controllers;

public class AuthController : Controller
{
	[Route("/signup")] // Så kan gå direkt till signup i stället för /auth/signup/
	[HttpGet]
	public IActionResult SignUp()
	{
		var viewModel = new SignUpViewModel();
		return View(viewModel);
	}

	[Route("/signup")] 
	[HttpPost]
	public IActionResult SignUp(SignUpViewModel viewModel)
	{
		if (!ModelState.IsValid) 
			return View(viewModel);

		return RedirectToAction("SignIn", "Auth");
	}


}
