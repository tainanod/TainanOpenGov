using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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

    [Authorize(Roles = "Admin, Office")]
    public class ParticipationController : Controller
    {
        private readonly int pageSize = 10;
        private readonly IParticipationService service;
        private readonly IPhotoService photoService;
        private readonly IParticipationDocumentService documentService;
        private readonly IParticipationHyperlinkService hyperlinkService;
        private readonly IAdmin.IDataSetService dataSetService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="photoService"></param>
        /// <param name="documentService"></param>
        /// <param name="hyperlinkService"></param>
        /// <param name="dataSetService"></param>
        public ParticipationController(IParticipationService service, IPhotoService photoService,
            IParticipationDocumentService documentService, IParticipationHyperlinkService hyperlinkService,
            IAdmin.IDataSetService dataSetService)
        {
            this.service = service;
            this.photoService = photoService;
            this.documentService = documentService;
            this.hyperlinkService = hyperlinkService;
            this.dataSetService = dataSetService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 取得列表資料
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult PartialList(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            IQueryable<Participation> data = service.GetQuery().OrderByDescending(x => x.OpenDate);
            var result = data.ToPagedList(currentPage, pageSize);
            ViewBag.unitList = dataSetService.GetUnitList().Select(x => new Entity.ViewModel.Admin.PageNameVm
            {
                PageId = x.PageGuid.ToString(),
                PageName = x.PageName
            }).ToList();
            return PartialView(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitId"></param>
        private void GetDataSetUnit(string unitId = null)
        {
            var data = dataSetService.GetUnitList().Select(x => new Entity.ViewModel.Admin.PageNameVm
            {
                PageId = x.PageGuid.ToString(),
                PageName = x.PageName
            });
            var unitData = string.IsNullOrEmpty(unitId) ?
                new SelectList(data, "PageId", "PageName") :
                new SelectList(data, "PageId", "PageName", unitId);
            ViewData["DataSetUnitId"] = unitData;

            ViewData["EnableDiscussStr"] = new List<ListItem>() {
                new ListItem("開放", "true"),
                new ListItem("不開放", "false"),
            };
        }

        /// <summary>
        /// 建立頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            GetDataSetUnit();
            var data = new Participation();
            data.OpenDate = DateTime.Now;
            data.CloseDate = DateTime.Now;
            return View(data);
        }
        
        /// <summary>
        /// 建立資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Participation model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");

            if (!ModelState.IsValid || (model.CloseDate < model.OpenDate))
            {
                GetDataSetUnit();
                if (model.CloseDate < model.OpenDate)
                {
                    ViewBag.Error = "截止時間不能小於發佈時間";
                }
                return View(model);
            }

            var result = service.Create(model);
            if (result.Success)
            {
                return RedirectToAction("Edit", new { id = result.Data.ParticipationId });
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// 編輯頁面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            var model = service.GetData(id);
            GetDataSetUnit(model.DataSetUnitId.ToString());
            return View(model);
        }
        
        /// <summary>
        /// 更新 資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Participation model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid || (model.CloseDate < model.OpenDate))
            {
                GetDataSetUnit(model.DataSetUnitId.ToString());
                if (model.CloseDate < model.OpenDate)
                {
                    ViewBag.Error = "截止時間不能小於發佈時間";
                }
                return View(model);
            }

            var result = service.UpdateData(model);
            if (result.Success)
            {
                return RedirectToAction("Edit", new { id = result.Data.ParticipationId });
            }
            else
            {
                GetDataSetUnit(model.DataSetUnitId.ToString());
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }
        
        /// <summary>
        /// 移除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Remove(Guid id)
        {
            Result<Participation> result = service.RemoveData(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }

        /// <summary>
        /// 封存或取消封存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Mothball(Guid id)
        {
            Result<Participation> result = service.MothballData(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }

        /// <summary>
        /// 圖片上傳頁面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PhotoUpload(Guid id)
        {
            return View();
        }

        /// <summary>
        /// 圖片 上傳
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postedFile"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PhotoUpload(Guid id, HttpPostedFileBase postedFile, string alt)
        {
            Result<Photo> result = photoService.PhotoUpload(postedFile, alt);
            if (result.Success)
            {
                bool success = service.addPhoto(id, result.Data);
                if (success == false)
                {
                    result.Success = false;
                }
                else
                {
                    result.Data = new Photo()
                    {
                        PhotoId = result.Data.PhotoId
                    };
                }
            }
            return View(result);
        }
        
        /// <summary>
        /// 附件上傳頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult DocumentUpload()
        {
            return View();
        }

        /// <summary>
        /// 附件上傳
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postedFile"></param>
        /// <param name="name"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
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

            Result<ParticipationDocument> result = documentService.Upload(id, uploadDoc);

            if (result.Success)
            {
                result.Data = null;
            }

            return View(result);
        }

        /// <summary>
        /// 移除 文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveDocument(Guid id)
        {
            var result = documentService.Remove(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }

        /// <summary>
        /// 取得 附件資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PartialDocument(Guid id)
        {
            IQueryable<ParticipationDocument> result = service.GetDocumentWithType(id, DocType.一般文件);
            return PartialView(result);
        }

        /// <summary>
        /// 市府回應
        /// </summary>
        /// <returns></returns>
        public ActionResult ResponseUpload()
        {
            return View();
        }

        /// <summary>
        /// 市府回應 附件 上傳
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postedFile"></param>
        /// <param name="name"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
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

            Result<ParticipationDocument> result = documentService.Upload(id, uploadDoc);

            if (result.Success)
            {
                result.Data = null;
            }

            return View(result);
        }

        /// <summary>
        /// 取得 市府回應 附件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PartialResponse(Guid id)
        {
            var result = service.GetDocumentWithType(id, DocType.市府回應);
            return PartialView(result);
        }

        /// <summary>
        /// 刪除 連結
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveHyperlink(Guid id)
        {
            Result<ParticipationHyperlink> result = hyperlinkService.Remove(id);
            result.Data = null;
            result.Exception = null;
            return Json(result);
        }

        /// <summary>
        /// 超連結 清單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PartialHyperlink(Guid id)
        {
            IQueryable<ParticipationHyperlink> result = service.GetHyperlink(id);
            return PartialView(result);
        }
        
        /// <summary>
        /// 超連結 建立頁面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CreateHyperlink(Guid id)
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHyperlink(Guid id, ParticipationHyperlink model)
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
                result.Data.Participations = null;
            }
            ViewBag.Result = result;
            return View(result.Data);
        }

        /// <summary>
        /// 市政參與議題資料匯出
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PartialExport(Guid id)
        {
            return View();
        }

        /// <summary>
        /// 市政資料 論壇匯出
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
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
    }
}