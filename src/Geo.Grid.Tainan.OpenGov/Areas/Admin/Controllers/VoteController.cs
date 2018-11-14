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
    /// 投票管理
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class VoteController : Controller
    {
        private readonly IVoteService _service;
        private int _pageSize = 20;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service"></param>
        public VoteController(IVoteService service)
        {
            _service = service;
        }

        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(SearchVm key)
        {
            ViewBag.key = key;
            var data = _service.GetList(key);
            var vmD = data.ToPagedList(key.Page, _pageSize);

            var forumData = _service.GetForumDetail(key.Id);
            ViewBag.forumData = forumData;

            return View(vmD);
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _AjaxIndex()
        {
            return View();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="id">forumId</param>
        /// <returns></returns>
        public ActionResult Create(Guid id)
        {
            ViewBag.forumId = id;
            var data = new VoteVm();

            //個資分類
            ViewBag.groupData = _service.GetGroup();

            //驗證方式
            ViewBag.verifyType = _service.GetVerifyType();

            return View(data);
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(VoteVm vmD)
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
        /// 編輯-單筆
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            var data = _service.GetDetail(id);

            //個資分類
            ViewBag.groupData = _service.GetGroup();

            //選項
            ViewBag.optionData = _service.GetOption(id).ToList();

            //驗證方式
            ViewBag.verifyType = _service.GetVerifyType();

            return View(data);
        }

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(VoteVm vmD, List<GuidNameVm> vmDx)
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
        /// 刪除
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public ActionResult Remove(Guid id)
        {
            var data = _service.GetRemove(id);
            return RedirectToAction("Index", new { id = data.ID });
        }

        #region 投票結果-統計圖表

        /// <summary>
        /// 圖表
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public ActionResult Gather(Guid id)
        {
            var data = _service.GetGather(id);

            //個資
            var basicData = _service.GetGatherBasic(id);
            ViewBag.basicData = basicData;

            if (data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(data);
        }

        #endregion

        #region 匯出

        /// <summary>
        /// 匯出
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public FileResult Excel(Guid id)
        {
            var data = _service.GetExcel(id);
            return File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "投票報表.xlsx");
        }

        #endregion

    }
}