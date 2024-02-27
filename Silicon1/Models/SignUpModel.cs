using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Silicon1.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Silicon1.Models;

public class SignUpModel
{
	[DataType(DataType.Text)]
	[Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
	[Required(ErrorMessage = "First name is required")]
	[MinLength(2, ErrorMessage = "Enter a first name")]
	public string FirstName { get; set; } = null!;

	[DataType(DataType.Text)]
	[Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
	[Required(ErrorMessage = "Last name is required")]
	[MinLength(2, ErrorMessage = "Enter a last name")]
	public string LastName { get; set; } = null!;

	[Display(Name = "Email", Prompt = "Enter your email address", Order = 2)]
	[DataType(DataType.EmailAddress)]
	[Required(ErrorMessage = "Email address is required")]
	[RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address")]
	public string Email { get; set; } = null!;

	[Display(Name = "Password", Prompt = "Enter your password", Order = 3)]
	[DataType(DataType.Password)]
	[Required(ErrorMessage = "Password is required")]
	[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Invalid password. must be a strong password")]
	public string Password { get; set; } = null!;

	[Display(Name = "Confirm Password", Prompt = "Confirm password", Order = 4)]
	[DataType(DataType.Password)]
	[Required(ErrorMessage = "Password must be confirmed")]
	[Compare(nameof(Password), ErrorMessage = "Password does not match")]
	public string ConfirmPassword { get; set; } = null!;

	[Display(Name = "I agree to the Terms & Condítions.", Order = 5)]
	[CheckBoxRequired(ErrorMessage = "You must accept the terms and conditions.")]
	public bool TermsAndConditions { get; set; } = false;
}