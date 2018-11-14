using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    public class TestController : Controller
    {
        // GET: Admin/Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}