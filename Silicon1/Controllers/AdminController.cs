using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Silicon1.Controllers;

[Authorize(Policy = "Admins")]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

	[Authorize(Policy = "SuperAdmins")]
	public IActionResult Settings()
    {
        return View();
    }
}
