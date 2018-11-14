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
    /// <summary>
    /// 公民論壇- 子議題
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class SubForumController : Controller
    {
        private readonly int _pageSize = 10;
        private readonly IForumService _service;
        private readonly IDocumentService documentService;
        private readonly Service.Interface.Admin.IDataSetService dataSetService;

        public SubForumController(IForumService service, IDocumentService documentService, Service.Interface.Admin.IDataSetService dataSetService)
        {
            this._service = service;
            this.documentService = documentService;
            this.dataSetService = dataSetService;
        }
        
        public ActionResult Index(Guid id)
        {
            var forum = _service.GetForum(id);
            ViewBag.Forum = forum;
            return View();
        }

        public ActionResult PartialList(Guid id, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            IQueryable<Forum> forums = _service.GetSubForums(id).OrderByDescending(x => x.OpenDate); 
            var result = forums.ToPagedList(currentPage, _pageSize);
            return PartialView(result);
        }

        public ActionResult Create(Guid id)
        {
            ViewData["Holder"] = GetDataSetUnit();
            return View();
        }

        private SelectList GetDataSetUnit(string selectItem = null)
        {
            var data = dataSetService.GetUnitList().Select(x => new Entity.ViewModel.Admin.PageNameVm
            {
                PageId = x.PageName.ToString(),
                PageName = x.PageName
            });
            return string.IsNullOrEmpty(selectItem) ? new SelectList(data, "PageId", "PageName") : new SelectList(data, "PageId", "PageName", selectItem);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid id, Forum model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            ModelState.Remove("DataSetUnitId");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.ForumType = ForumType.公民論壇;
            model.UpperId = id;
            var result = _service.Create(model, true);
            if (result.Success)
            {
                return RedirectToAction("Index", new { id = id });
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Edit(Guid id, Guid fid)
        {
            var model = _service.GetForum(id);
            ViewBag.ForumId = fid;
            ViewData["Holder"] = GetDataSetUnit(model.Holder);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Forum model, HttpPostedFileBase[] files)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            ModelState.Remove("DataSetUnitId");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _service.UpdateForum(model, true);
            if (result.Success)
            {
                return RedirectToAction("Index", new { id = result.Data.UpperId });
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
            Result<Forum> result = _service.RemoveForum(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }

        [HttpGet]
        public ActionResult UploadDocument(Guid? id, Guid fid)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Forum");
            }


            ViewData["mainForumId"] = fid;

            var documents = _service.GetDocumentWithType(id.Value, DocType.一般文件);


            return View(documents);
        }

        public ActionResult DocUploadPartial(Guid? id, HttpPostedFileBase file, string alt, string name)
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }

            var uploadDoc = new UploadDoc()
            {
                PostedFile = file,
                Name = name,
                Alt = alt,
                DocType = DocType.一般文件
            };
            Result<Document> result = documentService.Upload(id.Value, uploadDoc);
            if (result.Success)
            {
                result.Data = null;
            }

            return View(result);
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