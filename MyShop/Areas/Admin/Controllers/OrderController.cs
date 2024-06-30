using BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace MyShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        public OrderController(IOrderService orderService,ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }
        public IActionResult Index(bool isDeleted)
        {
            var list= _orderService.GetAll(isDeleted);
            return View(list);
        }
        public IActionResult Details(int Id)
        {
            var entity = _orderService.GetById(Id);
            ViewBag.Cart= _cartService.GetHistory(entity.CookieId);
            return View(entity);
        }
    }
}
