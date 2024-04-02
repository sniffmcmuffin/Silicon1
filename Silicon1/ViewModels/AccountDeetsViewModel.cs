using idInfrastructure.Entities;
using Silicon1.Models;
using Silicon1.ViewModels.Account;
using System.ComponentModel.DataAnnotations;

namespace Silicon1.ViewModels;

public class AccountDeetsViewModel
{
	public ProfileInfoViewModel? ProfileInfo { get; set; } 
	public string Title { get; set; } = "Account Details";
	public BasicInfoFormViewModel? BasicInfoForm { get; set; } 
	public AddressInfoFormViewModel? AddressInfoForm { get; set; } 
}
