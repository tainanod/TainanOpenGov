using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    /// <summary>
    /// 標籤管理
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class ParticipationTagController : Controller
    {
        private readonly IParticipationTagService _service;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public ParticipationTagController(IParticipationTagService service)
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

            var participationData = _service.GetParticipationDetail(key.Id);
            ViewBag.participationData = participationData;

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
            var data = new ParticipationTagVm();
            data.ParticipationId = id;
            return View(data);
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(ParticipationTagVm vmD)
        {
            var data = _service.GetCreate(vmD);
            if (!data.Success)
            {
                TempData["msgErr"] = data.Message;
                return RedirectToAction("Index", new { id = vmD.ParticipationId });
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
        public ActionResult Edit(ParticipationTagVm vmD)
        {
            var data = _service.GetEdit(vmD);
            if (!data.Success)
            {
                TempData["msgErr"] = data.Message;
                return RedirectToAction("Index", new { id = vmD.ParticipationId });
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