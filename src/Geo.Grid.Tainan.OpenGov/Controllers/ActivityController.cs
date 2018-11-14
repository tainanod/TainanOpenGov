using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService service;

        public ActivityController(IActivityService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Detail(Guid id)
        {
            ActivityVm act = service.GetActivityById(id);
            return Json(act);
        }
    }
}