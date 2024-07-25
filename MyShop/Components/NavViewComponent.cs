using BLL.Service;
using BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Components
{
	[ViewComponent(Name = "NavViewComponent")]
	public class NavViewComponent : ViewComponent
	{
		private readonly ICategoryService _categoryService;
        public NavViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
		{
			var list=await _categoryService.GetAll();

			return View(list);
		}
	}
}