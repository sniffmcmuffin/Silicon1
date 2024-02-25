using System.ComponentModel.DataAnnotations;

namespace Silicon1.Models;

public class AccountsDeetsAddressInfoModel
{
	[Display(Name = "First name", Prompt = "Enter your address line", Order = 0)]
	[Required(ErrorMessage = "First name is required")]
	public string Addressline_1 { get; set; } = null!;

	[Display(Name = "Last name", Prompt = "Enter your second address line", Order = 1)]
	[Required(ErrorMessage = "Last name is required")]
	public string Addressline_2 { get; set; } = null!;

	[Display(Name = "Postal Code", Prompt = "Enter your postal code", Order = 2)]
	[Required(ErrorMessage = "Postal Code is required")]
	[DataType(DataType.PostalCode)]
	public string PostalCode { get; set; } = null!;

	[Display(Name = "City", Prompt = "Enter your city", Order = 3)]
	[Required(ErrorMessage = "City is required")]
	public string City { get; set; } = null!;
}
