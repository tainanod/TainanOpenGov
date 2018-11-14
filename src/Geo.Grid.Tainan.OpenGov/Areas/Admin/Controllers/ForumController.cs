using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Common;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Newtonsoft.Json;
using PagedList;
using IAdmin = Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin,Office")]
    public class ForumController : Controller
    {
        private readonly int pageSize = 10;
        private readonly IForumService service;
        private readonly IPhotoService photoService;
        private readonly IDocumentService documentService;
        private readonly IHyperlinkService hyperlinkService;
        private readonly IAdmin.IDataSetService dataSetService;

        public ForumController(IForumService service, IPhotoService photoService,
            IDocumentService documentService, IHyperlinkService hyperlinkService,
            IAdmin.IDataSetService dataSetService)
        {
            this.service = service;
            this.photoService = photoService;
            this.documentService = documentService;
            this.hyperlinkService = hyperlinkService;
            this.dataSetService = dataSetService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialList(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            IQueryable<Forum> forums = service.QueryForum(ForumType.公民論壇).OrderByDescending(x => x.OpenDate);
            var result = forums.ToPagedList(currentPage, pageSize);
            return PartialView(result);
        }

        public ActionResult Create()
        {
            GetDataSetUnit();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Forum model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid)
            {
                GetDataSetUnit();
                return View(model);
            }
            model.ForumType = ForumType.公民論壇;
            var result = service.Create(model);
            if (result.Success)
            {
                return RedirectToAction("Edit", new { id = result.Data.ForumId });
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Edit(Guid id)
        {
            var model = service.GetForum(id);
            GetDataSetUnit(model.DataSetUnitId.Value.ToString());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Forum model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid)
            {
                GetDataSetUnit(model.DataSetUnitId.Value.ToString());
                return View(model);
            }

            var result = service.UpdateForum(model);
            if (result.Success)
            {
                return RedirectToAction("Edit", new { id = result.Data.ForumId });
            }
            else
            {
                GetDataSetUnit(model.DataSetUnitId.Value.ToString());
                ModelState.AddModelError("", result.Message);

                return View(model);
            }
        }

        [HttpPost]
        public JsonResult RemoveForum(Guid id)
        {
            Result<Forum> result = service.RemoveForum(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }

        public ActionResult PhotoUpload(Guid id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult PhotoUpload(Guid id, HttpPostedFileBase postedFile, string alt)
        {
            Result<Photo> result = photoService.PhotoUpload(id, postedFile, alt);
            if (result.Success)
            {
                result.Data = new Photo()
                {
                    PhotoId = result.Data.PhotoId
                };
            }

            return View(result);
        }

        public ActionResult PartialDocument(Guid id)
        {
            IQueryable<Document> result = service.GetDocumentWithType(id, DocType.一般文件);
            return PartialView(result);
        }



        public ActionResult DocumentUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DocumentUpload(Guid id, HttpPostedFileBase postedFile, string name, string alt)
        {
            var uploadDoc = new UploadDoc()
            {
                PostedFile = postedFile,
                Name = name,
                Alt = alt,
                DocType = DocType.一般文件
            };
            Result<Document> result = documentService.Upload(id, uploadDoc);
            if (result.Success)
            {
                result.Data = null;
            }

            return View(result);
        }

        public ActionResult PartialResponse(Guid id)
        {
            IQueryable<Document> result = service.GetDocumentWithType(id, DocType.市府回應);
            return PartialView(result);
        }
        public ActionResult ResponseUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResponseUpload(Guid id, HttpPostedFileBase postedFile, string name, string alt)
        {
            var uploadDoc = new UploadDoc()
            {
                PostedFile = postedFile,
                Name = name,
                Alt = alt,
                DocType = DocType.市府回應
            };
            Result<Document> result = documentService.Upload(id, uploadDoc);
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

        public ActionResult CreateHyperlink(Guid id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHyperlink(Guid id, Hyperlink model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = service.CreateHyperlink(id, model);
            if (result.Data != null)
            {
                result.Data.Forum = null;
            }
            ViewBag.Result = result;
            return View(result.Data);
        }

        public ActionResult PartialHyperlink(Guid id)
        {
            IQueryable<Hyperlink> result = service.GetHyperlink(id);
            return PartialView(result);
        }

        [HttpPost]
        public JsonResult RemoveHyperlink(Guid id)
        {
            Result<Hyperlink> result = hyperlinkService.Remove(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }

        public ActionResult PartialExport(Guid id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult PartialExport(Guid id, DateTime startDate, DateTime endDate)
        {
            var result = service.GetForumDiscuss(id, startDate, endDate);
            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            var dt = JsonConvert.DeserializeObject<DataTable>(result.Data.GetValue("Data").ToString());
            var name = result.Data.GetValue("Name").ToString();
            var exportFileName = string.Concat(
                name,
                "_",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                ".xlsx");

            return new ExportExcelResult
            {
                SheetName = name,
                FileName = exportFileName,
                ExportData = dt
            };
        }

        #region 取得業務單位
        private void GetDataSetUnit()
        {
            GetDataSetUnit("");
        }

        private void GetDataSetUnit(string setId)
        {
            var data = dataSetService.GetUnitList().Select(x => new Entity.ViewModel.Admin.PageNameVm
            {
                PageId = x.PageGuid.ToString(),
                PageName = x.PageName
            });
            var unitData = string.IsNullOrEmpty(setId) ?
                new SelectList(data, "PageId", "PageName") :
                new SelectList(data, "PageId", "PageName", setId);
            ViewData["DataSetUnitId"] = unitData;
        }
        #endregion
    }
}