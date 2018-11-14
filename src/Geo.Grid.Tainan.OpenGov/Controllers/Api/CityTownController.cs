using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class CityTownController : ApiController
    {
        private readonly ICityTownService townService;

        public CityTownController(ICityTownService townService)
        {
            this.townService = townService;
        }

        [HttpGet]
        public IHttpActionResult GetCities()
        {
            return Ok(townService.GetCities());
        }

        [HttpGet]
        [Route("api/citytown/gettowns/{id}")]
        public IHttpActionResult GetTowns(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("City ID Null");
            }

            return Ok(townService.GetTowns(id.Value));
        }

        #region  共用-New

        /// <summary>
        /// 縣市
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/CityTown/City")]
        public IHttpActionResult GetCity()
        {
            var data = townService.GetCity();
            return Ok(data);
        }

        /// <summary>
        /// 鄉鎮區
        /// </summary>
        /// <param name="id">townId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/CityTown/Town/{id}")]
        public IHttpActionResult GetTown(int id)
        {
            var data = townService.GetCityTown(id);
            return Ok(data);
        }

        #endregion
    }
}
