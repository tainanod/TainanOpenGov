using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    /// <summary>
    /// 市政監督
    /// </summary>
    public class EngineeringController : Controller
    {
        /// <summary>
        /// 地圖展點
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Detail
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            return View();
        }
    }

}