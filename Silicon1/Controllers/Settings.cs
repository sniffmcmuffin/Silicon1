using Microsoft.AspNetCore.Mvc;

namespace Silicon1.Controllers
{
	public class Settings : Controller
	{
		public IActionResult ChangeTheme(string theme)
		{
			var option = new CookieOptions
			{
				Expires = DateTime.Now.AddDays(60),
			};

			Response.Cookies.Append("ThemeMode", theme, option);
			return Ok();
		}
	}
}
