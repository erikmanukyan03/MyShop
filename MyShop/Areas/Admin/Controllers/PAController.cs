using BLL.Service;
using BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace MyShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PAController : Controller
    {
        private readonly IPAService _paService;
        public PAController(IPAService paService)
        {
            _paService = paService;
        }
        public IActionResult Index()
        {
            var list = _paService.GetAll();
            return View(list);
        }
        [HttpGet]
        public IActionResult AddEdit(int? Id)
        {
            var model = Id.HasValue?_paService.GetById(Id.Value):new PAVM();
            if (Id > 0)
            {
                ViewBag.Values= string.Join(',', model.Values.Select(p=>p.Value));
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult AddEdit(PAVM model, string values)
        {
           if(model.Id == 0)
            {
                model.AttributeType = "a";
                var paid = _paService.Add(model);
                model.Id = paid;
                var list = values.Split(',');
                foreach (var item in list)
                {
                    var entity = new PAVVM
                    {
                        ProductAttributeId = paid,
                        Value = item,
                    };
                    model.Values.Add(entity);
                }
                _paService.Update(model);
            }
            else
            {
                var list = values.Split(',');
                foreach (var item in list)
                {
                    var entity = new PAVVM
                    {
                        ProductAttributeId = model.Id,
                        Value = item,
                    };
                    model.Values.Add(entity);
                }
                _paService.Update(model);
            }
            return RedirectToAction("Index", "PA", new { area = "Admin" });
        }
        public IActionResult Delete(int Id)
        {
            _paService.Delete(Id);
            return RedirectToAction("Index", "PA", new { area = "Admin" });
        }
    }
}
