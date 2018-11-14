using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Jwt;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class TokenController : ApiController
    {
        private readonly string _secretKey = System.Web.Configuration.WebConfigurationManager.AppSettings["jwtKey"];
        private readonly string _encry = System.Web.Configuration.WebConfigurationManager.AppSettings["jwtEncry"];

        /// <summary>
        /// 驗證Token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JwtPayload Get(string id)
        {
            return new JwtTokenCreator().VerifyJwtToken(_secretKey, id, _encry).Data;
        }

        /// <summary>
        /// 取得Token
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        //public string Get(string id, string appName)
        //{
        //    return new JwtTokenCreator().GenerateJwtToken(id, appName, "OpenGov", _secretKey, _encry, 3600).Data;
        //}

        
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}