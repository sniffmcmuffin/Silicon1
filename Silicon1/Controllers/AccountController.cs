using Microsoft.AspNetCore.Mvc;
using Silicon1.ViewModels;

namespace Silicon1.Controllers;

public class AccountController : Controller
{
	// private readonly AccountService _accountService;

	//public AccountController(AccountService accountService)
	//{
	//	_accountService = accountService;
	//}

	[Route("/account")]
	public IActionResult Deets() // Bättre använda en service så man skickar med id bara och inte hela modellen.
	{		
		var viewModel = new AccountDeetsViewModel();

		// viewModel.BasicInfo = _accountService.GetBasicInfo(); 
		// viewModel.AddressInfo = _accountService.AddressInfo();
		return View(viewModel); 
	}

	[HttpPost]
	public IActionResult BasicInfo(AccountDeetsViewModel viewModel)
	{

		// _accountService.SaveBasicInfo(viewModel.BasicInfo);

		return RedirectToAction(nameof(Deets)); // Gå till details när det är klart.
	}

	[HttpPost]
	public IActionResult AddressInfo(AccountDeetsViewModel viewModel)
	{
		return RedirectToAction(nameof(Deets), viewModel); // Gå till details när det är klart.
	}

}
