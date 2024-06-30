using BLL.Service;
using Domain.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        //[Route("{slug}")]
        public IActionResult Details(string slug,int count=1,string Memory= "")
        {
            var model = _productService.GetBySlug(slug);
            ViewBag.Count = count;
            ViewBag.Memory = Memory;
            ViewBag.FinalPrice = model.FinalPrice;
            return View(model);
        }
    }
}
 