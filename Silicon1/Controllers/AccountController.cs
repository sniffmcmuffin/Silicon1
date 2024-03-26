using idInfrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon1.ViewModels;

namespace Silicon1.Controllers;

public class AccountController(SignInManager<UserEntity> sigInManager, UserManager<UserEntity> userManager) : Controller
{
	// private readonly AccountService _accountService;
	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SignInManager<UserEntity> _sigInManager = sigInManager;

    //public AccountController(AccountService accountService)
    //{
    //	_accountService = accountService;
    //}

    [HttpGet] // Chansar på att det ska vara detta här.
    [Route("/account/deets")]
	public async Task<IActionResult> Deets() // Bättre använda en service så man skickar med id bara och inte hela modellen.
	{
		if (!_sigInManager.IsSignedIn(User))
			return RedirectToAction("SignIn", "auth");

		var userEntity = await _userManager.GetUserAsync(User);

		var viewModel = new AccountDeetsViewModel()
		{
			User = userEntity!
		};

		// viewModel.BasicInfo = _accountService.GetBasicInfo(); 
		// viewModel.AddressInfo = _accountService.AddressInfo();
		return View(viewModel); 
	}

	[HttpPost]
	public async Task<IActionResult> BasicInfo(AccountDeetsViewModel viewModel)
	{

		// _accountService.SaveBasicInfo(viewModel.BasicInfo);

		var result = await _userManager.UpdateAsync(viewModel.User);
		if (result.Succeeded)
		{
			ModelState.AddModelError("Failed to save data", "Unable to save changes");
			ViewData["ErrorMessage"] = "Unable to save data";
		}
		
	   return RedirectToAction("Deets", "Account", viewModel); // Gå till details när det är klart.
	}

	[HttpPost]
	public IActionResult AddressInfo(AccountDeetsViewModel viewModel)
	{
		return RedirectToAction(nameof(Deets), viewModel); // Gå till details när det är klart.
	}

}
