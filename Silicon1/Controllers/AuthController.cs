using idInfrastructure.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon1.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace Silicon1.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{	
	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SignInManager<UserEntity> _signInManager = signInManager;

	#region SignUp
	[HttpGet]
	[Route("/signup")]
	public IActionResult SignUp()
	{
       if (_signInManager.IsSignedIn(User))
		 	return RedirectToAction("Deets", "Account"); // Funkar!

		return View();
	}

	[HttpPost]
	[Route("/signup")]
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
				// Alternativt 
				// return RedirectToAction("Signin", "Auth");
			}
		}
		return View(viewModel);
	}
	#endregion

	#region SignIn
	[HttpGet]
	[Route("/signin")]
	public IActionResult SignIn(string returnUrl)
	{
		if (_signInManager.IsSignedIn(User))
			return RedirectToAction("Deets", "Account"); 

		ViewData["ReturnUrl"] = "account/deets"; // returnUrl ?? Url.Content("~/"); vill inte.

		return View();
	}



	[HttpPost]
	[Route("/signin")]
	public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
	{
		if (ModelState.IsValid)
		{
			var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);

			if (result.Succeeded)
			{
				if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					return RedirectToAction(returnUrl);

				return RedirectToAction("Deets", "Account");
			}
		}
		ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
		ViewData["ErrorMessage"] = "Incorrect email or password";
		return View(viewModel);
	}
	#endregion

	#region SignOut
	[HttpGet]
	[Route("/signout")]
	public new async Task<IActionResult> SignOut()
	{
		await _signInManager.SignOutAsync();
		return RedirectToAction("Index", "Home");
	}
	#endregion
}