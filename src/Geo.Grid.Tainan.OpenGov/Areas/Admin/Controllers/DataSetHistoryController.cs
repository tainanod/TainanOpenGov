using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;
using Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;
using PagedList;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    /// <summary>
    /// 野生台南-資料目錄-歷程管理
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class DataSetHistoryController : Controller
    {
        private readonly IDataSetHistoryService _service;
        private int _pageSize = 20;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public DataSetHistoryController(IDataSetHistoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public ActionResult Index(SearchVm key)
        {
            ViewBag.key = key;

            return View();
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _AjaxIndex(SearchVm key)
        {
            ViewBag.key = key;

            var data = _service.GetList(key);
            var vmD = data.ToPagedList(key.Page, _pageSize);

            return View(vmD);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(Guid id)
        {
            var data = new DataSetHistoryVm();
            data.SetId = id;

            return View(data);
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DataSetHistoryVm vmD)
        {
            var data = _service.GetCreate(vmD);
            if (!data.Success)
            {
                TempData["msgErr"] = data.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = data.ID });
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            var data = _service.GetDetail(id);
            return View(data);
        }

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(DataSetHistoryVm vmD)
        {
            var data = _service.GetEdit(vmD);
            if (!data.Success)
            {
                TempData["msgErr"] = data.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = data.ID });
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove(Guid id)
        {
            var data = _service.GetRemove(id);
            TempData["msgErr"] = data.Message;
            return Json(data);
        }

        /// <summary>
        /// 彈出-使用指南
        /// </summary>
        /// <param name="id">guid</param>
        /// <returns></returns>
        public ActionResult PopPartial(Guid id)
        {
            var data = _service.GetDetail(id);
            return View(data);
        }

    }
}