using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Microsoft.AspNet.Identity;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using System.IdentityModel.Tokens.Jwt;
using Geo.Grid.Jwt;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class DiscussController : ApiController
    {
        private readonly IDiscussService _service;
        private readonly IForumService _forumService;
        private readonly IUserService _userService;

        public DiscussController(IDiscussService service, IForumService forumService, IUserService userService)
        {
            this._service = service;
            this._forumService = forumService;
            this._userService = userService;
        }

        /// <summary>
        /// 新增留言/回覆
        /// </summary>
        /// <param name="discussVm">ViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Discuss/CreateDiscuss")]
        public IHttpActionResult CreateDiscuss(DiscussVm discussVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data Null");
            }

            var result = _service.CreateMessage(discussVm.ForumId, discussVm.Message.Replace("\n", "<br>"), discussVm.UserId, discussVm.TagIds, discussVm.UpperId);

            //限回覆時傳值
            if (result.Success && result.Data.UpperId.HasValue)
            {
                var mailData = _service.GetReplyEmail(result.Data.DiscussId);
            }

            return Ok(result);
        }

        ///// <summary>
        ///// 取得留言(第一層)
        ///// </summary>
        ///// <param name="forumId">forum ID</param>
        ///// <param name="currentPage">目前頁數</param>
        ///// <param name="pageSize">一頁幾筆</param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("Api/Discuss/GetDiscuss/{forumId}")]
        //public IHttpActionResult GetDiscuss(Guid forumId, int currentPage = 1, int pageSize = 10)
        //{
        //    if (forumId == Guid.Empty)
        //    {
        //        return BadRequest("Data NULL");
        //    }

        //    return Ok(_service.GetAllDiscussion(forumId, currentPage, pageSize));
        //}

        [HttpGet, HttpPost]
        [Route("Api/Discuss/GetDiscuss")]
        public IHttpActionResult GetDiscussNew(DiscussSearchVm key)
        {
            var data = _service.GetAllDiscussionNew(key);
            return Ok(data);
        }

        /// <summary>
        /// 取得回覆(第二層)
        /// </summary>
        /// <param name="discussId">要回覆的留言的ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Discuss/GetReply/{discussId}")]
        public IHttpActionResult GetReply(Guid discussId)
        {
            if (discussId == Guid.Empty)
            {
                return BadRequest("Data NULL");
            }

            return Ok(_service.GetReply(discussId));
        }

        /// <summary>
        /// 推文
        /// </summary>
        /// <param name="discussId">要推文的留言/回覆 ID</param>
        /// <param name="pushUserId">推文者的ID</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Api/Discuss/PushMessage")]
        public IHttpActionResult PushMessage(Guid discussId, Guid pushUserId)
        {
            if (discussId == Guid.Empty || pushUserId == Guid.Empty)
            {
                return BadRequest("Data NULL");
            }

            return Ok(_service.PushMessage(discussId, pushUserId));
        }

        /// <summary>
        /// 取得全部標籤
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Discuss/Tags/{forumId}")]
        public IHttpActionResult GetAllTags(Guid forumId)
        {
            return Ok(_service.GetAllTags(forumId));
        }

        /// <summary>
        /// 取得討論主題資料
        /// </summary>
        /// <param name="forumId">Forum ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Forum/Get/{forumId}")]
        public IHttpActionResult GetForum(Guid forumId)
        {
            if (forumId == Guid.Empty)
            {
                return BadRequest("Data NULL");
            }

            return Ok(_forumService.GetForumAndRelations(forumId));
        }

        /// <summary>
        /// 取得子討論主題
        /// </summary>
        /// <param name="superForumId">父討論主題 ForumId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Discuss/SubForum/{superForumId}")]
        public IHttpActionResult GetSubForum(Guid superForumId)
        {
            if (superForumId == Guid.Empty)
            {
                return BadRequest("Data NULL");
            }

            return Ok(_service.GetSubForum(superForumId));
        }

        /// <summary>
        /// 取得User Data
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Discuss/User")]
        public IHttpActionResult GetUser()
        {
            var result = new Result<AspNetUserVm>();
            var _urlName = new WebSite().GetWebSite();
            var userID = User.Identity.GetUserId();
            if (userID == null)
            {
                result.Message = "Authorization Fail";
            }
            else
            {
                var context = _userService.GetUser(userID);
                result.Data = new AspNetUserVm
                {
                    Id = context.Id,
                    ImageUrl = (context.ImageUrl.Contains("http") ? context.ImageUrl : _urlName + "/" + context.ImageUrl.Replace("~/", "/")),
                    NickName = context.NickName
                };
                result.Success = true;
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("Api/Discuss/DemoTest")]
        public IHttpActionResult GetDemo()
        {
            var service = new Geo.Grid.Tainan.OpenGov.Service.WaitingMailService();
            var data = service.GetDiscussReplyAdminEmail();
            return Ok(data);
        }

        #region RESTful Api
        [HttpGet]
        [Route("Api/Discuss")]
        public IEnumerable<ApiDiscussVm> GetList()
        {
            return _service.GetList();
        }

        [HttpGet]
        [Route("Api/Discuss/{id}")]
        public IEnumerable<ApiDiscussDetailVm> GetDetail(Guid id, string keyword = "")
        {
            return _service.GetDetail(id, keyword);
        }

        [HttpPost]
        [Route("Api/Discuss")]
        public IHttpActionResult Create(ApiDiscussCreateVm vm)
        {
            var secretKey = System.Web.Configuration.WebConfigurationManager.AppSettings["jwtKey"];
            var encry = System.Web.Configuration.WebConfigurationManager.AppSettings["jwtEncry"];
            var jwt = new JwtTokenCreator().VerifyJwtToken(secretKey, vm.Token, encry);
            if (!jwt.Success || !jwt.Data.ContainsValue(vm.UserId))
            {
                return NotFound();
            }

            return Ok(_service.CreateMessage(vm.Id, vm.Message, vm.UserId, vm.TagIds));
        }

        [HttpGet]
        [Route("Api/SubDiscuss/")]
        public IEnumerable<ApiSubForumVm> GetSubforumList()
        {
            return _service.GetSubforumList();
        }

        [HttpPost]
        [Route("Api/SubDiscuss")]
        public IHttpActionResult SubCreate(ApiDiscussCreateVm vm)
        {
            var secretKey = System.Web.Configuration.WebConfigurationManager.AppSettings["jwtKey"];
            var encry = System.Web.Configuration.WebConfigurationManager.AppSettings["jwtEncry"];
            var jwt = new JwtTokenCreator().VerifyJwtToken(secretKey, vm.Token, encry);
            if (!jwt.Success || !jwt.Data.ContainsValue(vm.UserId))
            {
                return NotFound();
            }

            var discuss = _service.GetSubforumDetail(vm.Id).FirstOrDefault();
            if (discuss == null)
            {
                return NotFound();
            }

            return Ok(_service.CreateMessage(vm.Id, vm.Message, vm.UserId, vm.TagIds, discuss.Id));
        }

        [HttpGet]
        [Route("Api/SubDiscuss/{id}")]
        public IEnumerable<ApiSubForumDetailVm> GetSubforumDetail(Guid id)
        {
            return _service.GetSubforumDetail(id);
        }
        #endregion
    }
}
