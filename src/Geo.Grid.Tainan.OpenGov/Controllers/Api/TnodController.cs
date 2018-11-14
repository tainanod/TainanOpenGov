using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.JsonViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class TnodController : ApiController
    {
        private readonly ITnodService service;

        public TnodController(ITnodService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 政府資料
        /// </summary>
        /// <returns></returns>
        public List<TnodDataset> Get()
        {
            var result = service.GetTop5Dataset();
            return result;
        }

        /// <summary>
        /// 行程公開(目前行程公開不使用此API)
        /// </summary>
        /// <returns></returns>
        [Route("Api/Tnod/Top5Mayor")]
        [HttpGet]
        public List<TnodMayor> Top5Mayor()
        {
            var result = service.GetTop5Mayor();
            return result;
        }

        /// <summary>
        /// 行程公開(2018/07/03 界接新的[公開行程])
        /// </summary>
        /// <returns></returns>
        [Route("Api/Tnod/PublicSchedule")]
        [HttpGet]
        public List<PublicScheduleVm> PublicSchedule()
        {
            var result = service.GetPublicSchedule();
            return result;
        }

    }
}