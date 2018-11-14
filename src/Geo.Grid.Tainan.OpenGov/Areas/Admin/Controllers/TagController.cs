using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;
using Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    /// <summary>
    /// 標籤管理
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        private readonly ITagService _service;
        //private int _pageSize = 20;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public TagController(ITagService service)
        {
            _service = service;
        }

        /// <summary>
        /// 首頁
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public ActionResult Index(SearchVm key)
        {
            ViewBag.key = key;

            var data = _service.GetList(key);

            var forumData = _service.GetForumDetail(key.Id);
            ViewBag.forumData = forumData;

            return View(data);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _AjaxIndex(SearchVm key)
        {
            var data = _service.GetList(key);
            return View(data);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="id">forumId</param>
        /// <returns></returns>
        public ActionResult Create(Guid id)
        {
            var data = new TagVm();
            data.ForumId = id;

            return View(data);
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(TagVm vmD)
        {
            var data = _service.GetCreate(vmD);
            if (!data.Success)
            {
                TempData["msgErr"] = data.Message;
                return RedirectToAction("Index", new { id = vmD.ForumId });
            }
            return RedirectToAction("Edit", new { id = data.ID });
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="id">tagId</param>
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
        public ActionResult Edit(TagVm vmD)
        {
            var data = _service.GetEdit(vmD);
            if (!data.Success)
            {
                TempData["msgErr"] = data.Message;
                return RedirectToAction("Index", new { id = vmD.ForumId });
            }
            return RedirectToAction("Edit", new { id = data.ID });
        }

        /// <summary>
        /// 刪除-回傳forumId
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        public ActionResult Remove(Guid id)
        {
            var data = _service.GetRemove(id);
            TempData["msgErr"] = data.Message;
            return RedirectToAction("Index", new { id = data.ID });
        }

        /// <summary>
        /// 排序-升
        /// </summary>
        /// <param name="id">tagId</param>
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
        /// <param name="id">tagId</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SortDown(Guid id)
        {
            var data = _service.GetSortDown(id);
            return Json(data);
        }
    }
}