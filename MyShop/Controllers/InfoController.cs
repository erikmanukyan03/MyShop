using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
	public class InfoController : Controller
	{
		public IActionResult AboutUs()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
	}
}
