using BLL.Service;
using BLL.ViewModels;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using MyShop.Extensions;

namespace MyShop.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IEmailService _emailService;
        public OrderController(IOrderService orderService, ICartService cartService,IEmailService emailService)
        {
            _orderService= orderService;
            _cartService= cartService;
            _emailService= emailService;
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Carts = _cartService.GetByCookie(CookieId);
            ViewBag.CookieId = CookieId;
            return View();
        }
        [HttpPost]
        public /*async*/ IActionResult Create(OrderVM model)
        {
            model.CookieId = CookieId;
            model.Customer.CustomerType = CustomerType.Buyer;
            var orderId=_orderService.Add(model);
            _cartService.DeleteAll(CookieId);
            //var order = _orderService.GetById(orderId);
            //var body = await this.RenderViewToStringAsync("_EmailForOrder",order);
            //await _emailService.Send("Manukyn@gmail.com", body, $"Новый заказ! Номер заказа {orderId}");
            //if (model.Customer.Email != null)
            //{
            //    await _emailService.Send(model.Customer.Email, body, $"Ваш заказ № {orderId} получен");
            //}
            return RedirectToAction("Index", "Home");
        }
    }
}
