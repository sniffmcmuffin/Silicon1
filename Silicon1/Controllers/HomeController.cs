﻿using Microsoft.AspNetCore.Mvc;

namespace Silicon1.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() 
    {
		

		return View();
    }
}