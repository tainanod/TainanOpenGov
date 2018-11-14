using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    public class BudgetController : Controller
    {
        /// <summary>
        /// 歷年預算資料
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}