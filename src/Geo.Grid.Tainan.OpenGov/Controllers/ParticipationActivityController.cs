using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    public class ParticipationActivityController : Controller
    {
        private readonly IParticipationActivityService service;

        public ParticipationActivityController(IParticipationActivityService service)
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
            ParticipationActivityVm act = service.GetActivityById(id);
            return Json(act);
        }
    }
}