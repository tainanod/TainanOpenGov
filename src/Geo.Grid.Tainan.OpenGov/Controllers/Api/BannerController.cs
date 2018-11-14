using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    /// <summary>
    /// 形像圖管理
    /// </summary>
    public class BannerController : ApiController
    {
        private readonly IBannerService _service;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public BannerController(IBannerService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Banner/List")]
        public IHttpActionResult GetList()
        {
            var data = _service.GetList();
            return Ok(data);
        }
    }
}
