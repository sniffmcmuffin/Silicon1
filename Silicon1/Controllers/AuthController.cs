using Microsoft.AspNetCore.Mvc;

namespace Silicon1.Controllers;

public class AuthController : Controller
{
    public IActionResult SignUp()
    {
        return View();
    }
}
