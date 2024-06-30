using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    public class BaseController : Controller
    {
        protected string CookieId => Request.Cookies["UId"];
    }
}