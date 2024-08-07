using BLL.Service;
using BLL.ViewModels;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using MyShop.Captcha;
using MyShop.Extensions;
using System.Drawing.Imaging;

namespace MyShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IEmailService _emailService;
        public CustomerController(ICustomerService customerService,IEmailService emailService)
        {
            _customerService = customerService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerVM model, CustomerType role)
        {
            if(model.Captcha != HttpContext.Session.GetString("code"))
            {
                ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
                return Json(new { success = false, error = "captcha" });
            }
			if (!ModelState.IsValid)
			{
				return Json(new
				{
					success = false,
					errors = ModelState.ToDictionary(
					x => x.Key,
					x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
				)
				});
			}


			model.CustomerType = role;
            var cId = _customerService.Add(model);
            var customer = _customerService.GetById(cId);
            var body = await this.RenderViewToStringAsync("EmailForMe", customer);
            await _emailService.Send("manukyanerik.003@gmail.com", body, "Call Back");
            return Json(new { success = true });
        }
        [Route("captcha")]
        public IActionResult Captcha()
        {
            string code = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            HttpContext.Session.SetString("code", code);
            CaptchaImage captcha = new CaptchaImage(code, 110, 50);

            HttpContext.Response.Clear();
            this.Response.ContentType = "image/jpeg";
           MemoryStream stream = new MemoryStream();
            
            captcha.Image.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;
            captcha.Dispose();
            return File(stream, "image/jpeg");
        }
    }

}
