using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon1.Models.Components;
using Silicon1.Models.Sections;
using Silicon1.Models.Views;
using Silicon1.ViewModels;
using System.Text;

namespace Silicon1.Controllers;

public class HomeController(HttpClient httpClient) : Controller
{	
	private readonly HttpClient _httpClient = httpClient;

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

	[HttpPost]
	public async Task<IActionResult> Subscribe(SubscribeViewModel model)
	{
		if (ModelState.IsValid)
		{
			var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("https://localhost:7099/api/subscribe", content);
		
			if (response.IsSuccessStatusCode)
			{
				TempData["StatusMessage"] = "You are now subscribing.";
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
			{
				TempData["StatusMessage"] = "You are already subscribing.";
			}
		}
		else 
		{
			TempData["StatusMessage"] = "Invalid email address.";
		}

		return RedirectToAction("Index", "Home", "subscribe");
	}

	// använd sen nedan för delete
	// var response = await _httpClient.PostAsync($"https://localhost:7099/api/subscribe?email={email}", content);

	[Route("/error")]
	public IActionResult Error404(int statusCode) => View();

	[Route("/denied")]
	public IActionResult AccessDenied(int statusCode) => View();
}
