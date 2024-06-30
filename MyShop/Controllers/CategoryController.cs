using BLL.Service;
using BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public CategoryController(IProductService productService,ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _categoryService.GetForProducts();
            return View(list);
        }
        public async Task<IActionResult> Details(int Id,FilterVM? filter) 
        {
            var pr =await _categoryService.MinMaxPrice(Id);
            ViewBag.ProdMinPrice = pr.Item1;
            ViewBag.ProdMaxPrice = pr.Item2;
            if (filter.MinPrice == 0 && filter.MaxPrice == 0)
            {
                filter.MinPrice = pr.Item1 ?? 0;
                filter.MaxPrice = pr.Item2 ?? 0;
            }
            var li= await _categoryService.Filter(Id,filter);
            var a= await _productService.GetByCategory(Id);
            ViewBag.Pavs = a;
            ViewBag.Filter = filter;
            ViewBag.CategoryId = Id;
            ViewBag.Memories = await _productService.GetMemories(Id);
            var category = _categoryService.GetById(Id);
            var ctg = new CategoryVM
            {
                Name = category.Name,
                Description = category.Description,
                Products = li,
            };
            return View(ctg);
        }
    }
}