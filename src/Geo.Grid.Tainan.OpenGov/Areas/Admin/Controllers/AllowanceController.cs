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
    public class AllowanceController : Controller
    {
        private readonly int pageSize = 10;
        private readonly IAllowanceService allowanceService;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="iAllowanceService"></param>
        public AllowanceController(IAllowanceService iAllowanceService)
        {
            allowanceService = iAllowanceService;
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
        /// 清單分頁
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult PartialList(int page = 1)
        {
            var currentPage = page < 1 ? 1 : page;
            var result = allowanceService.GetAllowanceSourceList().ToPagedList(currentPage, pageSize);
            return PartialView(result);
        }

        /// <summary>
        /// 新增資料集來源管理 頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 新增資料集來源管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AllowanceSource model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Result<AllowanceSource> result = allowanceService.CreateAllowanceSource(model);
            if (result.Success)
            {
                return RedirectToAction("Index", new { id = result.Data.SourceId });
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// 編輯 資料集來源管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            AllowanceSource model = allowanceService.GetAllowanceSource(id);
            return View(model);
        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AllowanceSource model)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("UpdatedDate");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Result<AllowanceSource> result = allowanceService.UpdateAllowanceSource(model);
            if (result.Success)
            {
                return RedirectToAction("Index", new { id = result.Data.SourceId });
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }

        /// <summary>
        /// 刪除 資料集來源管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Remove(Guid id)
        {
            Result<AllowanceSource> result = allowanceService.RemoveAllowanceSource(id);
            return Json(result);
        }

        /// <summary>
        /// 更新 Allowance
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RefreshAllowanceBySourceId(Guid id)
        {
            Result<string> result = allowanceService.RefreshAllowanceBySourceId(id);
            return Json(result);
        }
    }
}