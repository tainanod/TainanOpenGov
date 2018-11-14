using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    /// <summary>
    /// 投票管理
    /// </summary>
    public class VoteController : Controller
    {
        private readonly IVoteService _service;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public VoteController(IVoteService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 投票頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid id)
        {
            ViewBag.isComplete = false;
            var data = _service.GetDetail(id);
            if (data == null)
            {
                Session["msgError"] = "請重新操作";
                return RedirectToAction("Message");
            }
            var check = _service.GetDetailCheck(id, string.Empty);
            if (!check.Success)
            {
                Session["msgError"] = check.Message;
                if (data.VerifyType == 3)
                {
                    return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Detail", new { id = id }) });
                }
                if (!string.IsNullOrEmpty(check.Data))
                {
                    ViewBag.isComplete = true;
                    return View(data);
                }
                return RedirectToAction("Message");
            }
            TempData["msgEmail"] = TempData["msgEmail"];
            return View();
        }

        /// <summary>
        /// 確認是否為有效投票
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public ActionResult _Check(Guid id)
        {
            var data = _service.GetVerifyCheckEmailTwo(id);
            Session["msgError"] = data.Success ? "感謝您的投票，您的投票已成為有效投票，謝謝" : data.Message;
            return RedirectToAction("Message", new { id = id });
        }

        /// <summary>
        /// 投票結果頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Gather(Guid id)
        {
            var data = _service.GetGather(id);
            if (data.FirstOrDefault() != null && !data.FirstOrDefault().CanVote)
            {
                Session["msgError"] = "統計結果尚未開放";
                return RedirectToAction("Message");
            }
            return View();
        }

        /// <summary>
        /// 訊息頁
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public ActionResult Message(string id)
        {
            try
            {
                ViewBag.data = null;
                if (!string.IsNullOrEmpty(id))
                {
                    var data = _service.GetDetail(new Guid(id));
                    ViewBag.data = data;
                }
            }
            catch (Exception e)
            {
                var xx = e.Message;
            }
            return View();
        }

        /// <summary>
        /// 投票-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateSave(VoteSaveVm vmD)
        {
            var data = _service.GetCreate(vmD);
            ViewBag.msgError = data.Message;
            Session["msgError"] = data.Message;
            return Json(data);
            //return RedirectToAction("Message", new { id = data.ID });
        }
    }
}