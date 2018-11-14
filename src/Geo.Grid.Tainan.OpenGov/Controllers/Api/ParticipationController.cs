using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class ParticipationController : ApiController
    {
        private readonly IParticipationService service;
        private readonly IParticipationDiscussService discussService;

        public ParticipationController(IParticipationService service, IParticipationDiscussService discussService, IUserService userService)
        {
            this.service = service;
            this.discussService = discussService;
        }

        public IEnumerable<ApiParticipationVm> Get()
        {
            return service.GetList();
        }

        public ApiParticipationDetailVm Get(Guid id) {
            return service.GetDetail(id);
        }
        
    }
}