using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KnowledgeController : Controller
    {
        // GET: Admin/Knowledge
        public ActionResult Index()
        {
            return View();
        }
    }
}