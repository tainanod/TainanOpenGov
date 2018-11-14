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
    /// 形像圖管理
    /// </summary>
    [Authorize(Roles = "Admin, Office")]
    public class BannerController : Controller
    {
        private readonly IBannerService _service;
        private int _pageSize = 20;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public BannerController(IBannerService service)
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
            var data = new BannerVm();
            data.Target = true;
            data.IsEnabled = true;
            data.IsTopEnabled = false;

            return View(data);
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BannerVm vmD)
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
        public ActionResult Edit(BannerVm vmD)
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
    }
}