using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.DataSet;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    /// <summary>
    /// 資料目錄
    /// </summary>
    public class DataSetController : ApiController
    {
        private readonly IDataSetService _service;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public DataSetController(IDataSetService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("Api/DataSet")]
        public IHttpActionResult GetList() {
            return Ok(_service.GetList());
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/DataSet/List")]
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
        [Route("Api/DataSet/Detail/{id}")]
        public IHttpActionResult GetDetail(Guid id)
        {
            var data = _service.GetDetail(id);
            return Ok(data);
        }

        [HttpGet]
        [Route("Api/DataSet/{id}")]
        public IHttpActionResult GetDataSetDetail(Guid id) {
            return Ok(_service.GetDataSetDetail(id));
        }

        /// <summary>
        /// 類別
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/DataSet/Type")]
        public IHttpActionResult GetTypeList()
        {
            var data = _service.GetTypeList();
            return Ok(data);
        }
    }
}
