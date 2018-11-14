using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.ShowCase;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    /// <summary>
    /// 野生台南-應用展示
    /// </summary>
    public class ShowCaseController : ApiController
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

        [HttpGet]
        [Route("Api/ShowCase")]
        public IHttpActionResult GetList() {
            return Ok(_service.GetList());
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/ShowCase/List")]
        public IHttpActionResult GetList(SearchVm key)
        {
            var data = _service.GetList(key);
            return Ok(data);
        }


        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/ShowCase/Detail/{id}")]
        public IHttpActionResult GetDetail(Guid id)
        {
            var data = _service.GetDetail(id);
            return Ok(data);
        }

        /// <summary>
        /// 資料目錄
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/ShowCase/DataSet")]
        public IHttpActionResult GetDataSet()
        {
            var data = _service.GetDataSetList();
            return Ok(data);
        }

        /// <summary>
        /// 使用者資料
        /// </summary>
        /// <returns></returns>
        [HttpPost] 
        [Route("Api/ShowCase/UserDetail")]
        public IHttpActionResult GetUserDetail()
        {
            var data = _service.GetUserDetail();
            return Ok(data);
        }
    }
}
