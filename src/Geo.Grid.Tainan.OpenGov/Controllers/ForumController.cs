﻿using System;
using System.IO;
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
    public class ForumController : Controller
    {
        private readonly IForumService service;
        private readonly IDiscussService discussService;
        private readonly IUserService userService;

        public ForumController(IForumService service, IDiscussService discussService, IUserService userService)
        {
            this.service = service;
            this.discussService = discussService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var forums = service.GetForumByType(ForumType.公民論壇);

            return View(forums);
        }

        public ActionResult Detail(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult SubDetail(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
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

        [HttpGet]
        public ActionResult download()
        {
            var bytes = System.IO.File.ReadAllBytes(Server.MapPath("../App_Data/臺南市政府新市政中心評估指標及候選地點基本資料.pdf"));

            //return File(bytes, "application/pdf", "臺南市政府新市政中心評估指標及候選地點基本資料.pdf");
            return File(bytes, "application/pdf");
        }
    }
}