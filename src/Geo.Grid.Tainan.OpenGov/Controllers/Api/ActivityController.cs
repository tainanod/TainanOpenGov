using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class ActivityController : ApiController
    {
        private readonly IActivityService _service;
        private readonly string _urlName = new WebSite().GetWebSite();

        public ActivityController(IActivityService service)
        {
            _service = service;
        }

        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public IEnumerable<ApiActivityVm> Get(Guid id)
        {
            return _service.GetForumActivitiesByType(id, Entity.Enumeration.ActivityType.論壇活動)
                           .OrderByDescending(x => x.HoldDate).ToList()
                           .Select(x => new ApiActivityVm {
                               Id = x.Forum.ForumId,
                               Name = x.Forum.Subject,
                               ActName = x.Subject,
                               HoldDate = x.HoldDate,
                               Location = x.Location,
                               Description = x.Description,
                               Documents = x.Document.Where(d=>d.IsEnabled && d.DocumentType == Entity.Enumeration.DocType.一般文件)
                                            .Select(d=>new Entity.ViewModel.PageNameVm {
                                                PageGuid = d.DocumentId,
                                                PageName = d.FileName,
                                                PageUrl = $"{_urlName}/Document/{d.DocumentId}"
                                            })
                           }).ToList();
        }

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