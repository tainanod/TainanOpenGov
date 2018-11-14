using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    public class ParticipationHyperlinkController : Controller
    {
        private readonly IParticipationHyperlinkService _service;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public ParticipationHyperlinkController(IParticipationHyperlinkService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ActionResult Detail(Guid id)
        {
            var data = _service.GetDetail(id);
            if (data != null)
            {
                var vmD = _service.GetHyperlinkSave(id);
                if (data.Url.StartsWith("https://"))
                {
                    return Redirect("https://" + data.Url.Replace("https://", ""));
                }
                else
                {
                    return Redirect("http://" + data.Url.Replace("http://", ""));
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}