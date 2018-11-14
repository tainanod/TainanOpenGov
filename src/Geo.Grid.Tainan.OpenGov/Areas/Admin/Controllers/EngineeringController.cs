using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using PagedList;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Office")]
    public class EngineeringController : Controller
    {
        private readonly int pageSize = 10;

        /// <summary>
        /// service
        /// </summary>
        private readonly IEngineeringService engineeringService;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="iEngineeringService"></param>
        public EngineeringController(IEngineeringService iEngineeringService)
        {
            engineeringService = iEngineeringService;
        }

        /// <summary>
        /// 清單頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpgradeEngineering(HttpPostedFileBase file)
        {
            BinaryReader binaryReader = new BinaryReader(file.InputStream);
            string csvMessage = Encoding.Default.GetString(binaryReader.ReadBytes(file.ContentLength));
            int row = engineeringService.UpgradeEngineering(file.FileName, csvMessage, User.Identity.Name);
            Result<string> result = new Result<string>();
            result.Message = $"更新{row}筆";
            result.Success = row > 0;
            return Json(result);
        }

        /// <summary>
        /// 刪除 資料集來源管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Remove(long id)
        {
            return Json(engineeringService.Remove(id));
        }

        /// <summary>
        /// 清單分頁
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult PartialList(int page = 1)
        {
            var currentPage = page < 1 ? 1 : page;
            try
            {
                var result = engineeringService.GetEngineeringLogs().ToPagedList(currentPage, pageSize);
                return PartialView(result);
            }
            catch (Exception e)
            {
                string a = "";
                return null;
            }
        }

        /// <summary>
        /// 下載原始的 CSV資料
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public ActionResult DownloadCsv(int logId)
        {
            try
            {
                var engineeringLogVm = engineeringService.DownloadCsv(logId);
                
                return File(Encoding.Default.GetBytes(engineeringLogVm.LogMessage), "application/octet-stream", engineeringLogVm.FileName);
            }
            catch (Exception ex)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        /// <summary>
        /// 下載 市政監督 工程標案
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadEngineering()
        {
            try
            {
                var data = Encoding.UTF8.GetString(engineeringService.DownloadEngineering());
                var engineeringData = Encoding.Default.GetBytes(data);

                return File(engineeringData, "application/octet-stream", "市政監督_工程標案.csv");
            }
            catch (Exception ex)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

    }
}