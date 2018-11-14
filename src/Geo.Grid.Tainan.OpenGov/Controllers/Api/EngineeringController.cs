using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Engineering;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    /// <summary>
    /// 工程管考
    /// </summary>
    public class EngineeringController : ApiController
    {
        /// <summary>
        /// Service
        /// </summary>
        private readonly IEngineeringService engineeringService;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="iEngineeringService"></param>
        public EngineeringController(IEngineeringService iEngineeringService)
        {
            engineeringService = iEngineeringService;
        }

        /// <summary>
        /// 關鍵字搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>工程資料</returns>
        [HttpPost]
        [Route("Api/Engineering/Query")]
        public IHttpActionResult Query(EngineeringQueryVm queryVm)
        {
            //return Ok(engineeringService.Query(queryVm));
            return Ok(engineeringService.GetGeoJson(queryVm, false));
        }

        /// <summary>
        /// 進階搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>工程資料</returns>
        [HttpPost]
        [Route("Api/Engineering/AdvQuery")]
        public IHttpActionResult AdvQuery(EngineeringQueryVm queryVm)
        {
            //return Ok(engineeringService.AdvQuery(queryVm));
            return Ok(engineeringService.GetGeoJson(queryVm, true));

        }

        /// <summary>
        /// 明細資料
        /// </summary>
        /// <returns>工程資料</returns>
        [HttpGet]
        [Route("Api/Engineering/Detail/{govCode}/{code}")]
        public IHttpActionResult Detail(string govCode, string code)
        {
            return Ok(engineeringService.Detail(govCode, code));
        }

        /// <summary>
        /// 取得 行政區
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Engineering/GetTown")]
        public IHttpActionResult GetTowns()
        {
            return Ok(engineeringService.GetTown());
        }

        /// <summary>
        /// 取得 工程類別
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Engineering/GetCategory")]
        public IHttpActionResult GetCategory()
        {
            return Ok(engineeringService.GetCategory());
        }

        ///// <summary>
        ///// 取得 狀態 (因狀態項目固定不變，所以直接由前端處理，後台不提供。)
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("Api/Engineering/GetStatus")]
        //public IHttpActionResult GetStatus()
        //{
        //    return Ok(engineeringService.GetStatus());
        //}

        /// <summary>
        /// 取得 工程進度
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Engineering/GetProgressText")]
        public IHttpActionResult GetProgressText()
        {
            return Ok(engineeringService.GetProgressText());
        }
    }
}
