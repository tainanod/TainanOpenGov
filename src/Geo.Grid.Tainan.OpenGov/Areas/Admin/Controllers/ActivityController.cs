using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using PagedList;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActivityController : Controller
    {
        private readonly int pageSize = 10;
        private readonly IActivityService service;
        private readonly IForumService forumService;
        private readonly IDocumentService documentService;

        public ActivityController(IActivityService service, IForumService forumService, IDocumentService documentService)
        {
            this.service = service;
            this.forumService = forumService;
            this.documentService = documentService;
        }

        [HttpPost]
        public JsonResult Remove(Guid id)
        {
            Result<Activity> result = service.Remove(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }

        #region 論壇活動

        public ActionResult ActIndex(Guid id)
        {
            var forum = forumService.GetForum(id);
            return View(forum);
        }

        public ActionResult ActList(Guid id, int page = 1)
        {
            var currentPage = page < 1 ? 1 : page;
            var act = service.GetForumActivitiesByType(id, ActivityType.論壇活動).OrderByDescending(x => x.HoldDate).ToList();
            var result = act.ToPagedList(currentPage, pageSize);
            return PartialView(result);
        }

        public ActionResult ActNew(Guid id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ActNew(Guid id, Activity model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.ActivityType = ActivityType.論壇活動;
            var result = service.CreateActivity(id, model);
            if (result.Data != null)
            {
                result.Data.Forum = null;
            }
            ViewBag.Result = result;
            return View(result.Data);
        }

        #endregion 論壇活動

        #region 工作坊

        public ActionResult WorkIndex(Guid id)
        {
            var forum = forumService.GetForum(id);
            return View(forum);
        }

        public ActionResult WorkList(Guid id, int page = 1)
        {
            var currentPage = page < 1 ? 1 : page;
            var act = service.GetForumActivitiesByType(id, ActivityType.工作坊).OrderByDescending(x => x.HoldDate).ToList();
            var result = act.ToPagedList(currentPage, pageSize);
            return PartialView(result);
        }

        public ActionResult WorkNew(Guid id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkNew(Guid id, Activity model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.ActivityType = ActivityType.工作坊;
            var result = service.CreateActivity(id, model);
            if (result.Data != null)
            {
                result.Data.Forum = null;
            }
            ViewBag.Result = result;
            return View(result.Data);
        }

        #endregion 工作坊

        public ActionResult ActEdit(Guid id)
        {
            var model = service.GetActivity(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ActEdit(Guid id, Activity model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = service.UpdateActivity(id, model);
            result.Data = null;

            ViewBag.Result = result;
            return View(result.Data);
        }

        public ActionResult Document(Guid id)
        {
            IQueryable<Document> result = service.GetDocument(id);
            ViewBag.Result = null;
            return View(result);
        }

        public ActionResult DocumentUpload(Guid id)
        {
            IQueryable<Document> model = service.GetDocument(id);
            return View("Document", model);
        }

        [HttpPost]
        public ActionResult DocumentUpload(Guid id, HttpPostedFileBase postedFile, string name, string alt, DocType docType)
        {
            var uploadDoc = new UploadDoc()
            {
                PostedFile = postedFile,
                Name = name,
                Alt = alt,
                DocType = docType
            };
            Result<Document> result = documentService.UploadForActivity(id, uploadDoc);
            if (result.Success)
            {
                result.Data = null;
            }
            ViewBag.Result = result;
            IQueryable<Document> model = service.GetDocument(id);
            return View("Document", model);
        }

        [HttpPost]
        public JsonResult RemoveDocument(Guid id)
        {
            Result<Document> result = documentService.Remove(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }
    }
}