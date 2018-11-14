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
    /// 投票管理
    /// </summary>
    public class VoteController : ApiController
    {
        private readonly IVoteService _service;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public VoteController(IVoteService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="id">公民論壇編號</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Vote/{id}")]
        public IHttpActionResult GetList(Guid id)
        {
            var data = _service.GetList(id);
            return Ok(data);
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Vote/Detail/{id}")]
        public IHttpActionResult GetDetail(Guid id)
        {
            var data = _service.GetDetail(id);
            return Ok(data);
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Vote/Create")]
        public IHttpActionResult GetCreate(VoteSaveVm vmD)
        {
            var data = _service.GetCreate(vmD);
            return Ok(data);
        }

        #region 統計圖表-後台用

        /// <summary>
        /// 圖表
        /// </summary>
        /// <param name="id">VoteId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Vote/Gather/{id}")]
        public IHttpActionResult GetGather(Guid id)
        {
            var data = _service.GetGather(id);
            return Ok(data);
        }

        /// <summary>
        /// 圖表-個資
        /// </summary>
        /// <param name="id">VoteId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Vote/Gather/Basic/{id}")]
        public IHttpActionResult GetGatherBasic(Guid id)
        {
            var data = _service.GetGatherBasic(id);
            return Ok(data);
        }

        /// <summary>
        /// 圖表-個資-單筆
        /// </summary>
        /// <param name="id">voteId</param>
        /// <param name="groupId">groupId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Vote/Gather/BasicDetail/{id}")]
        public IHttpActionResult GetGatherBasicDetail(Guid id, int groupId)
        {
            var data = _service.GetGatherBasicDetail(id, groupId);
            return Ok(data);
        }

        #endregion

        #region 共用

        /// <summary>
        /// 縣市
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Vote/City")]
        public IHttpActionResult GetCity()
        {
            var data = _service.GetCity();
            return Ok(data);
        }

        /// <summary>
        /// 縣市/鄉鎮區
        /// </summary>
        /// <param name="id">cityId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Vote/CityTown")]
        public IHttpActionResult GetCityTown()
        {
            var data = _service.GetCityTown();
            return Ok(data);
        }

        #endregion

    }
}
