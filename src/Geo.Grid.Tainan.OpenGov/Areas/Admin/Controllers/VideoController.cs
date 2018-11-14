using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using PagedList;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Office")]
    public class VideoController : Controller
    {
        private readonly int pageSize = 10;
        private readonly IVideoService service;

        public VideoController(IVideoService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialList(int page = 1)
        {
            var currentPage = page < 1 ? 1 : page;
            IQueryable<Youtube> videos = service.QueryVideo().OrderByDescending(x => x.StartDate);
            var result = videos.ToPagedList(currentPage, pageSize);
            return PartialView(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Youtube model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Result<Youtube> result = service.Create(model);
            if (result.Success)
            {
                return RedirectToAction("Index", new { id = result.Data.YoutubeId });
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Edit(Guid id)
        {
            Youtube model = service.GetYoutube(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Youtube model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Result<Youtube> result = service.Update(model);
            if (result.Success)
            {
                return RedirectToAction("Index", new { id = result.Data.YoutubeId });
            }
            else
            {
                ModelState.AddModelError("", result.Message);

                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Remove(Guid id)
        {
            Result<Youtube> result = service.Remove(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }
    }
}