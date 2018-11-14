﻿using System;
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
    /// 野生台南-資料目錄-類別
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class DataSetTypeController : Controller
    {
        private readonly IDataSetTypeService _service;
        private int _pageSize = 20;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public DataSetTypeController(IDataSetTypeService service)
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
        public ActionResult Create()
        {
            var data = new DataSetTypeVm();
            return View(data);
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DataSetTypeVm vmD)
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
        public ActionResult Edit(DataSetTypeVm vmD)
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
        /// 排序-升
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SortUp(Guid id)
        {
            var data = _service.GetSortUp(id);
            return Json(data);
        }

        /// <summary>
        /// 排序-降
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SortDown(Guid id)
        {
            var data = _service.GetSortDown(id);
            return Json(data);
        }

    }
}