using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class RssController : ApiController
    {
        private readonly IRssService service;

        public RssController(IRssService service)
        {
            this.service = service;
        }

        public List<RssEntity> Get()
        {
            var result = service.GetTop5Rss();
            return result;
        }
    }
}