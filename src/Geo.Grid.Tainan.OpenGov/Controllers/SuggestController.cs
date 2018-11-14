using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.Security.Application;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    /// <summary>
    /// Suggest
    /// </summary>
    public class SuggestController : Controller
    {
        private readonly IForumService service;
        private readonly IDiscussService discussService;
        private readonly IUserService userService;

        public SuggestController(IForumService service, IDiscussService discussService, IUserService userService)
        {
            this.service = service;
            this.discussService = discussService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var forums = service.GetForumByType(ForumType.市政提案);

            return View(forums);
        }

        public ActionResult Detail(Guid id)
        {
            ViewBag.User = userService.GetUser(User.Identity.GetUserId());
            var model = service.GetForumWithRelation(id);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public JsonResult AddMessage(Guid id, string message)
        {
            message = message.Replace("\n", "<br/>");
            //AntiXSS
            message = Sanitizer.GetSafeHtmlFragment(message);

            Result<Discuss> result = discussService.AddMessage(id, message, User.Identity.GetUserId());
            result.Data = null;
            return Json(result);
        }

        public ActionResult DiscussPartial(Guid id)
        {
            IQueryable<Discuss> list = service.GetDiscuss(id);
            return PartialView(list);
        }
    }
}