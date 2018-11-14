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
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class CardController : ApiController
    {
        private readonly ICardService _service;
        private readonly IForumService _forumService;
        private readonly IParticipationService _participationService;

        public CardController(ICardService service, IForumService forumService, IParticipationService participationService)
        {
            _service = service;
            _forumService = forumService;
            _participationService = participationService;
        }

        /// <summary>
        /// Get 後台九宮格卡片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Card/Cards")]
        public List<CardListVm> Cards()
        {
            return _service.GetCards();
        }

        /// <summary>
        /// Get 九宮格卡片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Card/ApiCards")]
        public List<CardListVm> ApiCards()
        {
            return _service.GetApiCards();
        }

        /// <summary>
        /// 更新 九宮格卡片排序
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Card/UpdateCards")]
        public Result<List<CardListVm>> UpdateCards(List<CardListVm> cards)
        {
            return _service.UpdateCards(cards);
        }


        /// <summary>
        /// 刪除 九宮格卡片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Card/Delete/{id}")]
        public Result<string> Delete(Guid id)
        {
            return _service.DeleteCard(id);
        }
        /// <summary>
        /// 新增 九宮格卡片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Card/CreateCard")]
        public Result<CardVm> CreateCard(CardVm card)
        {
            return _service.CreateCard(card);
        }

        /// <summary>
        /// 編輯 九宮格卡片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Card/EditCard")]
        public Result<CardVm> EditCard(CardVm card)
        {
            return _service.EditCard(card);
        }

        /// <summary>
        /// 更新卡片內文
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Card/UpdateContents")]
        public Result<CardVm> UpdateContents(CardVm card)
        {
            return _service.UpdateContents(card);
        }

        /// <summary>
        /// 公民論壇
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Card/Forum")]
        public List<ForumVm> Forum()
        {
            return _forumService.GetForumByType(ForumType.公民論壇).Take(5).Select(x => new ForumVm()
            {
                Category = x.Category,
                ForumId = x.ForumId,
                Subject = x.Subject
            }).ToList();
        }


        /// <summary>
        /// 市政參與
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Card/Participation")]
        public List<ParticipationVm> Participation()
        {
            return _participationService.GetDataByType().Take(5).Select(x => new ParticipationVm()
            {
                Category = x.Category,
                ParticipationId = x.ParticipationId,
                Subject = x.Subject
            }).ToList();
        }


        /// <summary>
        /// 市政提案
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Card/Suggest")]
        public List<ForumVm> Suggest()
        {
            return _forumService.GetForumByType(ForumType.市政提案).Take(5).Select(x => new ForumVm()
            {
                Category = x.Category,
                ForumId = x.ForumId,
                Subject = x.Subject,
                DiscussCount = x.Discuss.Where(g=>g.IsEnabled).Count()
            }).ToList();
        }

    }
}
