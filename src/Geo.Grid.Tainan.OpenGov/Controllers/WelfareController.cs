using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    /// <summary>
    /// 公益台南
    /// </summary>
    public class WelfareController : Controller
    {
        /// <summary>
        /// 列表頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 詳細資料
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            return View();
        }


        public ActionResult Map()
        {
            return View();
        }
    }
}