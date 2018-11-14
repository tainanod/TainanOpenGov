using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class ForumController : ApiController
    {
        private readonly IForumService service;
        private readonly IDiscussService discussService;

        public ForumController(IForumService service, IDiscussService discussService, IUserService userService)
        {
            this.service = service;
            this.discussService = discussService;
        }
        // GET api/<controller>
        public IEnumerable<ApiForumVm> Get()
        {
            return service.GetList();
        }
        public ApiForumDetailVm Get(Guid id) {
            return service.GetDetail(id);
        }
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