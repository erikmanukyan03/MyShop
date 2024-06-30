using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using MyShop.Controllers;

namespace MyShop.Components
{
    [ViewComponent(Name = "BasketCountViewComponent")]
    public class BasketCountViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;
        public BasketCountViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cid = Request.Cookies["UId"];
            var count = _cartService.ProdsCount(cid);
            return View(count);
        }
    }
}
