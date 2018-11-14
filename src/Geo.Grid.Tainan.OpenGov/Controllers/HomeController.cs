using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using System.Web.Security;
using System.Web;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITnodService tnodService;
        private readonly IRssService rssService;
        private readonly IVideoService videoService;
        private readonly IForumService forumService;

        public HomeController(ITnodService tnodService, IRssService rssService, IVideoService videoService, IForumService forumService)
        {
            this.tnodService = tnodService;
            this.rssService = rssService;
            this.videoService = videoService;
            this.forumService = forumService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Office")]
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 政府資料 partial
        /// </summary>
        /// <returns></returns>
        public ActionResult TnodPartial()
        {
            var result = tnodService.GetTop5Dataset();
            return PartialView(result);
        }
        /// <summary>
        /// 行程公開
        /// </summary>
        /// <returns></returns>

        public ActionResult MayorPartial()
        {
            var result = tnodService.GetTop5Mayor();
            return PartialView(result);
        }

        /// <summary>
        /// 警報告示 RSS界接
        /// </summary>
        /// <returns></returns>
        public ActionResult RssPartial()
        {
            var result = rssService.GetTop5Rss();
            return PartialView(result);
        }

        public ActionResult YoutubePartial()
        {
            var result = videoService.GetVideo();
            ViewBag.date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return PartialView(result);
        }

        public ActionResult ForumPartial()
        {
            var result = forumService.GetForumByType(ForumType.公民論壇).Take(5);
            return PartialView(result);
        }

        public ActionResult ShowcasePartial()
        {
            return PartialView();
        }

        public ActionResult SuggestPartial()
        {
            var result = forumService.GetForumByType(ForumType.市政提案).Take(2);
            return PartialView(result);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}