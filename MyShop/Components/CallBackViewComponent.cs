using BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Components
{
    [ViewComponent(Name = "CallBackViewComponent")]
    public class CallBackViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new CustomerVM();

            return View(model);
        }


    }
}
