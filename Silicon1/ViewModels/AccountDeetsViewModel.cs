using Silicon1.Models;

namespace Silicon1.ViewModels;

public class AccountDeetsViewModel
{
	public string Title { get; set; } = "Account Details";
	public AccountDeetsBasicInfoModel BasicInfo { get; set; } = new AccountDeetsBasicInfoModel()
	{
		ProfileImage = "images/profile-image.svg",
		FirstName = "Jimmy",
		LastName = "Sjöström",
		Email = "jimmy@domain.nu"
	};
	public AccountsDeetsAddressInfoModel AddressInfo { get; set; } = new AccountsDeetsAddressInfoModel();
	
	// Här kan man ha filuppladdning för sin profilbild.
}
