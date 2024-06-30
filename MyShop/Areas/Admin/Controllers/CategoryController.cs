using BLL.Service;
using BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MyShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(bool isDeleted)
        {
            var list = await _categoryService.GetAll(isDeleted);
            return View(list);
        }
        [HttpGet]
        public IActionResult AddEdit(int? Id)
        {
            var entity = Id.HasValue ? _categoryService.GetForEdit(Id.Value) : new CategoryAddEditVM();
            return View(entity);
        }
        [HttpPost]
        public IActionResult AddEdit(CategoryAddEditVM model)
        {
            if (model.Id == 0)
            {
                _categoryService.Add(model);
            }
            else
            {
                _categoryService.Update(model);
            }
            return RedirectToAction("Index", new { area = "Admin" });
        }
        public IActionResult Delete(int Id)
        {
            _categoryService.Delete(Id);
            return RedirectToAction("Index", new { area = "Admin" });
        }
    }
}
