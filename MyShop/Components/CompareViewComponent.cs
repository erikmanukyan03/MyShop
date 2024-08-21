using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using MyShop.Controllers;

namespace MyShop.Components
{
    [ViewComponent(Name = "CompareViewComponent")]
    public class CompareViewComponent : ViewComponent
    {
        private readonly ICompareSevice _compareService;
        public CompareViewComponent(ICompareSevice compareService)
        {
            _compareService = compareService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cid = Request.Cookies["UId"];
            var count = _compareService.GetCount(cid);
            return View(count);
        }
    }
}
