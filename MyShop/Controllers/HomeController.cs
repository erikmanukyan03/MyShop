using BLL.Service;
using BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using System.Diagnostics;

namespace MyShop.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
		{
			ViewBag.Hots = await _productService.GetHots();
            return View();
		}
		[HttpPost]
        public async Task<IActionResult> Search(string keyword)
		{
			var list = await _productService.GetAll(null, false);
			var newlist=new List<ShortProductVM>();
			var splited = keyword?.Split(" ");
			if(splited.Length > 0) {
				newlist=await _productService.Search(list,splited);
			}
            return View(newlist);
		}
        [Route("sitemap.xml")]
        public IActionResult SiteMap()
        {
            var model = _productService.GetProductUrls();
            return PartialView(model);
        }
    }
}