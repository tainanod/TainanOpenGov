using System;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    /// <summary>
    /// 公益台南 - 弱勢補助
    /// </summary>
    public class AllowanceController : ApiController
    {
        private readonly IAllowanceService allowanceService;

        /// <summary>
        /// 建立弱勢補助 Controller
        /// </summary>
        /// <param name="iAllowanceService">弱勢補助 service</param>
        public AllowanceController(IAllowanceService iAllowanceService)
        {
            allowanceService = iAllowanceService;
        }

        /// <summary>
        /// 關鍵字搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>補助 資料</returns>
        [HttpPost]
        [Route("Api/Allowance/Query")]
        public IHttpActionResult Query(AllowanceQueryVm queryVm)
        {
            return Ok(allowanceService.Query(queryVm));
        }

        /// <summary>
        /// 進階搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>補助 資料</returns>
        [HttpPost]
        [Route("Api/Allowance/AdvQuery")]
        public IHttpActionResult AdvQuery(AllowanceQueryVm queryVm)
        {
            return Ok(allowanceService.AdvQuery(queryVm));
        }

        /// <summary>
        /// 取得 明細
        /// </summary>
        /// <param name="allowanceId">補助ID</param>
        /// <returns>補助 資料</returns>
        [HttpGet]
        [Route("Api/Allowance/Detail/{allowanceId}")]
        public IHttpActionResult Detail(Guid allowanceId)
        {
            return Ok(allowanceService.Detail(allowanceId));
        }

        /// <summary>
        /// 區公所
        /// </summary>
        /// <returns>區公所 資料</returns>
        [HttpGet]
        [Route("Api/Allowance/GetDistrictOffice")]
        public IHttpActionResult GetDistrictOffice()
        {
            return Ok(allowanceService.GetDistrictOffice());
        }

        /// <summary>
        /// 請問您是否具備下列特殊身分條件?
        /// </summary>
        /// <returns>特殊身分條件</returns>
        [HttpGet]
        [Route("Api/Allowance/GetIdentity")]
        public IHttpActionResult GetIdentity()
        {
            return Ok(allowanceService.GetIdentity());
        }

        /// <summary>
        /// 請問您是否具備下列其他身分條件
        /// </summary>
        /// <returns>其他身分條件</returns>
        [HttpGet]
        [Route("Api/Allowance/GetOthers")]
        public IHttpActionResult GetOthers()
        {
            return Ok(allowanceService.GetOthers());
        }

        /// <summary>
        /// 更新 Allowance
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Allowance/RefreshAllowance")]
        public IHttpActionResult RefreshAllowance()
        {
            return Ok(allowanceService.RefreshAllowanceList());
        }

    }
}
