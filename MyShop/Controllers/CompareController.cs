using BLL.Service;
using BLL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
	public class CompareController : BaseController
	{
		private readonly ICompareSevice _compareSevice;
        public CompareController(ICompareSevice compareSevice)
        {
            _compareSevice = compareSevice;
        }
        public IActionResult Index()
		{
			var list = _compareSevice.GetAll(CookieId);
			return View(list);
		}
		public IActionResult Delete(int Id)
		{
			_compareSevice.Delete(Id);
			return RedirectToAction("Index");
		}
		public IActionResult DeleteAll(string cookieId,int categoryId)
		{
			_compareSevice.DeleteAll(cookieId,categoryId);
			return RedirectToAction("Index");
		}
		//public IActionResult Add(int prodId)
		//{

		//}

	}
}
