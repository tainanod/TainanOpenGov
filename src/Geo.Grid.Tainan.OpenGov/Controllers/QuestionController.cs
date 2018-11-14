using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    /// <summary>
    /// 問卷Controller
    /// </summary>
    public class QuestionController : Controller
    {
        private readonly IQuestionService questService;
        private readonly IUserService userService;
        private readonly ICityTownService townService;

        public QuestionController(IQuestionService questService, IUserService userService, ICityTownService townService)
        {
            this.questService = questService;
            this.userService = userService;
            this.townService = townService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("index", "forum");
        }

        public ActionResult TestUi()
        {
            return View();
        }

        /// <summary>
        /// 問卷結果頁
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public ActionResult Gather(Guid id)
        {
            var data = questService.GetQuestGather(id);
            if (data.FirstOrDefault() != null && !data.FirstOrDefault().IsGather)
            {
                Session["msgError"] = "統計結果尚未開放";
                return RedirectToAction("Message");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Detail(Guid id)
        {
            ViewBag.isComplete = false;
            var data = questService.GetQuestDetailNew(id);
            if (data == null)
            {
                Session["msgError"] = "請重新操作";
                return RedirectToAction("Message");
            }

            var check = questService.GetQuestDetailCheckNew(id, string.Empty);

            if (!check.Success)
            {
                Session["msgError"] = check.Message;
                if (data.VerifyType == 3 && !System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            
            ViewBag.cityData = townService.GetCity();
            ViewBag.townData = townService.GetCityTown(16);
            return View(data);
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Detail(QuestSaveVm vmD)
        {            
            var data = questService.GetQuestFillCreateNew(vmD);
            Session["msgError"] = data.Message;

            return RedirectToAction("Message", new { id = data.ID });
        }

        //[HttpPost]
        //public ActionResult Detail(Guid infoID, List<PostQuestFillVm> vm)
        //{
        //    var data = questService.GetQuestFillCreateNew(vm);
        //    Session["msgError"] = data.Message;
        //    return RedirectToAction("Message");
        //}

        /// <summary>
        /// 檢查回傳mail驗證
        /// </summary>
        /// <param name="id">voteId</param>
        /// <param name="eMail">email</param>
        /// <returns></returns>
        public ActionResult _Check(Guid id)
        {
            var data = questService.GetVerifyCheckEmailTwo(id);
            Session["msgError"] = data.Success ? "感謝您的填寫，您的問卷已成為有效問卷，謝謝" : data.Message;
            return RedirectToAction("Message", new { id = id });
        }

        /// <summary>
        /// 訊息頁
        /// </summary>
        /// <param name="id">questionId</param>
        /// <returns></returns>
        public ActionResult Message(string id)
        {
            try
            {
                ViewBag.data = null;
                if (!string.IsNullOrEmpty(id))
                {
                    var data = questService.GetQuestDetailNew(new Guid(id));
                    ViewBag.data = data;
                }
            }
            catch(Exception e)
            {
                var xx = e.Message;
            }
            return View();
        }
    }
}