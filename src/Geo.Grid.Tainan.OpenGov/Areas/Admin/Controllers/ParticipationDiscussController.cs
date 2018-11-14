using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;
using PagedList;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    /// <summary>
    /// 公民論壇-討論區
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class ParticipationDiscussController : Controller
    {
        private readonly IParticipationDiscussService _service;
        private int _pageSize = 20;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service"></param>
        public ParticipationDiscussController(IParticipationDiscussService service)
        {
            _service = service;
        }

        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(SearchVm key)
        {
            ViewBag.key = key;
            var data = _service.GetList(key);
            var vmD = data.ToPagedList(key.Page, _pageSize);

            var forumData = _service.GetParticipationDetail(key.id);
            ViewBag.forumData = forumData;

            return View(vmD);
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _AjaxIndex()
        {
            return View();
        }

        /// <summary>
        /// 置頂/非置頂
        /// </summary>
        /// <param name="id">discussId</param>
        /// <returns></returns>
        public ActionResult UpdateTop(Guid id)
        {
            var data = _service.GetUpdateTop(id);
            return RedirectToAction("Index", new { id = data.ID });
        }
    }
}