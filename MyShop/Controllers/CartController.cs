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
		[HttpPost]
		public IActionResult AddToBasket(int productId, int count, double FinalPrice, bool fromprodpage = false)
		{
			try
			{
				var model = _productService.GetById(productId);
				if (model == null)
				{
					return Json(new { success = false, message = "Product not found." });
				}

				var list = _cartService.GetByCookie(CookieId);
				if (list == null)
				{
					return Json(new { success = false, message = "Cart not found for the given cookie ID." });
				}

				var entity = list.FirstOrDefault(c => c.ProductId == productId);

				if (entity != null)
				{
					entity.Count += count;
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

				return Json(new { success = true, message = "Product added to cart successfully." });
			}
			catch (Exception ex)
			{
				// Log the exception (use your logging mechanism)
				return Json(new { success = false, message = "An error occurred: " + ex.Message });
			}
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
