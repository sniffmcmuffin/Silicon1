using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Silicon1.ViewModels;

namespace Silicon1.Controllers;

public class AuthController(UserService userService) : Controller
{
	private readonly UserService _userService = userService;

	[Route("/signup")] // Så kan gå direkt till signup i stället för /auth/signup/
	[HttpGet]
	public IActionResult SignUp()
	{
		var viewModel = new SignUpViewModel();
		return View(viewModel);
	}

	[Route("/signup")] 
	[HttpPost]
	public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
	{
		if (!ModelState.IsValid)
		{
			var result = await _userService.CreateUserAsync(viewModel.Form);

			if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
				return RedirectToAction("SignIn", "Auth");
		}
			return View(viewModel);
	}

	[Route("/signin")] 
	[HttpGet]
	public IActionResult SignIn()
	{
		var viewModel = new SignInViewModel();
		return View(viewModel);
	}

	[Route("/signin")]
	[HttpPost]
	public async Task<IActionResult> SignIn(SignInViewModel viewModel)
	{
		if (ModelState.IsValid) // Det är fler saker än detta som ska till för att göra inloggning enl Hans. Vilka?
		{
			var result = await _userService.SignInUserAsync(viewModel.Form);

			if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
				return RedirectToAction("Deets", "Account");
		}

		viewModel.ErrorMessage = "Incorrect email or password";
		return View(viewModel);			
	}
}