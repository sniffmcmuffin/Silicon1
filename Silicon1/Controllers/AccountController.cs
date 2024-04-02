using idInfrastructure.Entities;
using idInfrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon1.Models;
using Silicon1.ViewModels;
using Silicon1.ViewModels.Account;

namespace Silicon1.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, AddressService addressService) : Controller
{
	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly AddressService _addressService = addressService;

	#region [HttpGet] Details
	[HttpGet]
	[Route("/account/deets")]
	public async Task<IActionResult> Deets()
	{
		var viewModel = new AccountDeetsViewModel();
		viewModel.ProfileInfo = await PopulateProfileInfoAsync();
		viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
		viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();
		return View(viewModel);
	}
	#endregion

	#region [HttpPost]Details
	[HttpPost]
	[Route("/account/deets")]
	public async Task<IActionResult> Deets(AccountDeetsViewModel viewModel)
	{
		if (viewModel.BasicInfoForm != null)
		{
			if (viewModel.BasicInfoForm.FirstName != null &&
				viewModel.BasicInfoForm.LastName != null &&
				viewModel.BasicInfoForm.Email != null)
			{
				var user = await _userManager.GetUserAsync(User);

				if (user != null)
				{
					user.FirstName = viewModel.BasicInfoForm.FirstName;
					user.LastName = viewModel.BasicInfoForm.LastName;
					user.Email = viewModel.BasicInfoForm.Email;
					user.PhoneNumber = viewModel.BasicInfoForm.PhoneNumber;
					user.Bio = viewModel.BasicInfoForm.Biography;

					var result = await _userManager.UpdateAsync(user);

					if (!result.Succeeded)
					{
						ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save data.");
						ViewData["ErrorMessage"] = "Something went wrong! Unable to update basic information.";
					}
				}
			}
		}

		if (viewModel.AddressInfoForm != null)
		{
			if (viewModel.AddressInfoForm.AddressLine_1 != null &&
				viewModel.AddressInfoForm.PostalCode != null &&
				viewModel.AddressInfoForm.City != null)
			{
				var user = await _userManager.GetUserAsync(User);

				if (user != null)
				{
					var address = await _addressService.GetAddressAsync(user.Id);

					if (address != null)
					{
						address.AdressLine_1 = viewModel.AddressInfoForm.AddressLine_1;
						address.AdressLine_2 = viewModel.AddressInfoForm.AddressLine_2;
						address.PostalCode = viewModel.AddressInfoForm.PostalCode;
						address.City = viewModel.AddressInfoForm.City;

						var result = await _addressService.UpdateAddressAsync(address);

						if (!result)
						{
							ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save updated data.");
							ViewData["ErrorMessage"] = "Something went wrong! Unable to save updated data.";
						}
					}
					else
					{
						address = new AddressEntity
						{
							UserId = user.Id,
							AdressLine_1 = viewModel.AddressInfoForm.AddressLine_1,
							AdressLine_2 = viewModel.AddressInfoForm.AddressLine_2,
							PostalCode = viewModel.AddressInfoForm.PostalCode,
							City = viewModel.AddressInfoForm.City,
						};

						var result = await _addressService.CreateAddressAsync(address);

						if (!result)
						{
							ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save updated data.");
							ViewData["ErrorMessage"] = "Something went wrong! Unable to save updated data.";
						}
					}
				}
			}
		}

		viewModel.ProfileInfo = await PopulateProfileInfoAsync();
		viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
		viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();

		return View(viewModel);
	}
	#endregion

	private async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
	{
		var user = await _userManager.GetUserAsync(User);

		return new ProfileInfoViewModel
		{
			FirstName = user!.FirstName,
			LastName = user.LastName,
			Email = user.Email!
		};
	}

	private async Task<BasicInfoFormViewModel> PopulateBasicInfoAsync()
	{
		var user = await _userManager.GetUserAsync(User);

		return new BasicInfoFormViewModel
		{
			UserId = user!.Id,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email!,
			PhoneNumber = user.PhoneNumber,
			Biography = user.Bio,
		};
	}

	private async Task<AddressInfoFormViewModel> PopulateAddressInfoAsync()
	{
		var user = await _userManager.GetUserAsync(User);

		if (user != null)
		{
			var address = await _addressService.GetAddressAsync(user.Id);
			return new AddressInfoFormViewModel
			{
				AddressLine_1 = address.AdressLine_1,
				AddressLine_2 = address.AdressLine_2,
				PostalCode = address.PostalCode,
				City = address.City,
			};
		}

		return new AddressInfoFormViewModel();
	}
}