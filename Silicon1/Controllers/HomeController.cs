using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silicon1.Models.Components;
using Silicon1.Models.Sections;
using Silicon1.Models.Views;

namespace Silicon1.Controllers;

public class HomeController : Controller
{
	// [Authorize] // Smidigt om man vill tvinga folk vara inloggade.
	[Route("/")]
	public IActionResult Index()
	{
		var viewModel = new HomeIndexViewModel
		{
			Title = "Ultimate Task Management Assistant",
			ShowCase = new ShowCaseViewModel
			{
				Id = "showcase",
				ShowCaseImage = new() {  ImageUrl = "images/showcase-taskmaster.svg", AltText = "Task Management Assistant"},
				Title = "Task Management Assistant You Gonna Love",
				Text = "We offer you a new generation of task management system. Plan, manage and track all your tasks in one flexible tool.",
				Link = new() { ControllerName = "Downloads", ActionName = "Index", Text = "Get started for free!"},
				BrandsText = "Largest companies use our tool to work efficiently",
				Brands =
				[
					new() { ImageUrl = "/images/brand_1.svg", AltText = "Brand 1 logo" },
					new() { ImageUrl = "/images/brand_2.svg", AltText = "Brand 2 logo" },
					new() { ImageUrl = "/images/brand_3.svg", AltText = "Brand 3 logo" },
					new() { ImageUrl = "/images/brand_4.svg", AltText = "Brand 4 logo" }
				],
			}
		};

		ViewData["Title"] = viewModel.Title;
		return View(viewModel);
    }

	[Route("/error")]
	public IActionResult Error404(int statusCode) => View();

	[Route("/denied")]
	public IActionResult AccessDenied(int statusCode) => View();
}
