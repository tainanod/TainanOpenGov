using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Service;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    /// <summary>
    /// 寄信
    /// </summary>
    public class WaitingMailController : Controller
    {
        private WaitingMailService _service = new WaitingMailService();

        /// <summary>
        /// 信件管理-每日16:30寄信給管理者
        /// 回覆的
        /// </summary>
        /// <returns></returns>
        public JsonResult DiscussReplyAdminEmail()
        {
            var data = _service.GetDiscussReplyAdminEmail();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}