using idInfrastructure.Models;
using Silicon1.Models;

namespace Silicon1.ViewModels;

public class SignInViewModel
{
	public string Title { get; set; } = "Sign in";
	public SignInModel Form { get; set; } = new SignInModel();
	public string? ErrorMessage { get; set; }
}
