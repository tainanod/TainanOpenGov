using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.ShowCase;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    /// <summary>
    /// 野生台南-應用展示
    /// </summary>
    [Authorize]
    public class ShowCaseController : Controller
    {
        private readonly IShowCaseService _service;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public ShowCaseController(IShowCaseService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 上傳
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(ShowCaseCreateVm vmD)
        {
            var data = _service.GetCreate(vmD);
            return Json(data);
        }
    }
}