using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;
using Microsoft.AspNet.Identity;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    [Authorize]
    public class SharedController : Controller
    {
        private readonly ISharedService _sharedService;

        public SharedController(ISharedService sharedService)
        {
            _sharedService = sharedService;
        }

        [ChildActionOnly]
        public ActionResult _Menu()
        {
            var menus = _sharedService.GetMenu(User.Identity.GetUserId());
            return PartialView(menus);
        }
    }
}