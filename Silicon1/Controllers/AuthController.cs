using idInfrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon1.ViewModels;

namespace Silicon1.Controllers;

// public class AuthController(UserService userService) : Controller
public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
	// private readonly UserService _userService = userService;
	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [Route("/signup")] // Så kan gå direkt till signup i stället för /auth/signup/
	[HttpGet]
	public IActionResult SignUp()
	{
		var viewModel = new SignUpViewModel();
		return View(viewModel);
	}

	[Route("/signup")]
	[HttpPost]
	//public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
	//{
	//	if (!ModelState.IsValid)
	//	{
	//		var result = await _userService.CreateUserAsync(viewModel.Form);

	//		if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
	//			return RedirectToAction("SignIn", "Auth");
	//	}
	//		return View(viewModel);
	//}
	public async Task<IActionResult> Signup(SignUpViewModel viewModel)
	{
		if (ModelState.IsValid)
		{
			var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
			if (exists)
			{
				ModelState.AddModelError("Already exists", "User with same email address already exists");
				ViewData["ErrorMessage"] = "User with the same email address already exists";
				return View(viewModel);
			}

			var userEntity = new UserEntity
			{
				FirstName = viewModel.Form.FirstName,
				LastName = viewModel.Form.LastName,
				Email = viewModel.Form.Email,
				UserName = viewModel.Form.Email
			};

			var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);
			if (result.Succeeded)
			{
				return RedirectToAction("Deets", "Account");
			}
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
		//if (ModelState.IsValid) // Det är fler saker än detta som ska till för att göra inloggning enl Hans. Vilka?
		//{
		//	var result = await _userService.SignInUserAsync(viewModel.Form);

		//	if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
		//		return RedirectToAction("Deets", "Account");
		//}

		if (ModelState.IsValid)
		{
			var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);
			
			if (result.Succeeded)
			{
				return RedirectToAction("Deets", "Account");
			}
		}
		ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
		ViewData["ErrorMessage"] = "Incorrect email or password";
		return View(viewModel);
	}
}