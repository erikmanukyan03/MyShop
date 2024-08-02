using BLL.Service;
using BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace MyShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPAService _paService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHost;
        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHost, IPAService paService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHost = webHost;
            _paService = paService;
        }
        public IActionResult Index(bool IsDeleted)
        {
            var list =  _productService.GetAllForAdmin(null,IsDeleted);
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int? Id)
        {
            var entity = Id.HasValue ? _productService.GetById(Id.Value) : new();
            ViewBag.Categories =await _categoryService.GetAll();
            return View(entity);
        }
        [HttpPost]
        public IActionResult AddEdit(ProductVM model, IFormFile mainImage, List<IFormFile> Images)
        {
            if (Images == null)
            {
                model.Images = null;
            }
            else
            {
                model.Images = new();
                foreach (var item in Images)
                {
                    string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(item.FileName);
                    string path = $"{_webHost.WebRootPath}/assets/img/product/{fileName}";
                    model.Images.Add(new ProductImageVM()
                    {
                        ImageFile = fileName,
                        ProductId = model.Id
                    });
                    using var fileStream = new FileStream(path, FileMode.Create);
                    item.CopyTo(fileStream);
                }
            }
            if (mainImage == null)
            {
                model.MainImage = null;
            }
            else
            {
                var filename=Guid.NewGuid() + System.IO.Path.GetExtension(mainImage.FileName);
                var path = $"{_webHost.WebRootPath}/assets/img/product/{filename}";
                model.MainImage = filename;
                using var filestream=new FileStream(path, FileMode.Create);
                mainImage.CopyTo(filestream);
            }
            if (model.Id == 0)
            {
                _productService.Add(model);
            }
            else
            {
                _productService.Update(model);
            }
            return RedirectToAction("Index", new { area = "Admin" });
        }
        public IActionResult Details(int Id)
        {
           
            var entity = _productService.GetById(Id);
            ViewBag.CategoryName = _categoryService.GetById(entity.CategoryId).Name;
            //ViewBag.Attributes = _paService.GetByProdId(Id);
            return View(entity);
        }
        public IActionResult Delete(int Id)
        {
            _productService.Delete(Id);
            return RedirectToAction("Index", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult AddEditAttribute(int Id,bool edit)
        {
            var list=_paService.GetAll();
            ViewBag.Selected=_productService.GetById(Id).PAVVMs.Select(P=>P.Id).ToList();
            ViewBag.ProdId = Id;
            ViewBag.Edit = edit;
            return View(list);
        }
        [HttpPost]
        public IActionResult AddEditAttribute(int prodId, List<int> list,bool edit)
        {
            _productService.AddEditAttribute(prodId,list,edit);
            return RedirectToAction("Details",  new { Id = prodId });
        }
        public IActionResult DeleteImage(int Id,int imageId)
        {
            _productService.DeleteImage(imageId);
            return RedirectToAction("Details",new { Id = Id });
        }

    }
}
