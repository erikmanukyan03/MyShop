using BLL.Service;
using BLL.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }
        public ActionResult Delete(int Id) 
        {
            _cartService.Delete(Id);
            return RedirectToAction("Create","Order");
        }
        public ActionResult DeleteAll(string cookieId)
        {
            _cartService.DeleteAll(cookieId);
            return RedirectToAction("Create", "Order");
        }
        public IActionResult AddToBasket(int productId,int count,double FinalPrice, bool fromprodpage=false)
        {
            var model = _productService.GetById(productId);
            var list = _cartService.GetByCookie(CookieId);
            var entity=list.FirstOrDefault(c => c.ProductId == productId);
            if (entity != null)
            {
                entity.Count += count ;
                _cartService.Update(entity);
            }
            else
            {
                var cart = new CartVM
                {
                    IsDeleted = false,
                    CookieId = CookieId,
                    Count = count,
                    Price = FinalPrice,
                    ProductId = productId,
                };
                _cartService.Add(cart);
            }
            if (fromprodpage)
            {
                return RedirectToAction("Details", "Product", new { slug = model.Slug, count = count });
            }
            return RedirectToAction("Details", "Category", new { Id = model.CategoryId });
            
        }
        public IActionResult Update(int cartId,int count)
        {
            var cart=_cartService.GetForEdit(cartId);
            cart.Count = count;
            _cartService.Update(cart);
            return RedirectToAction("Create", "Order");

        }
    }
}
