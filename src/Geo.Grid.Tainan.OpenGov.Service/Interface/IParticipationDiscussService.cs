using System;
using System.Collections.Generic;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IParticipationDiscussService
    {
        Result<ParticipationDiscuss> AddMessage(Guid id, string message, string userId);

        Result<ParticipationDiscussVm> CreateMessage(Guid id, string message, string userId, List<Guid> tagIds, Guid? upperID = null);

        PageResult<ParticipationDiscussVm> GetAllDiscussion(Guid id, int currentPage, int pageSize);

        Result<List<ParticipationDiscussVm>> GetReply(Guid discussId);

        Result<ParticipationDiscussVm> PushMessage(Guid discussId, Guid pushUserId);

        /// <summary>
        /// 標籤管理
        /// </summary>
        /// <param name="forumId">forumId</param>
        /// <returns></returns>
        IQueryable<ParticipationTagVm> GetAllTags(Guid forumId);
        
        #region New

        /// <summary>
        /// 取得主題下的留言-New
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        PageResult<ParticipationDiscussVm> GetAllDiscussionNew(ParticipationDiscussSearchVm key);

        /// <summary>
        /// 信件管理-回覆時寄信
        /// UPPER_ID-要有值
        /// </summary>
        /// <param name="id">UPPER_ID</param>
        /// <returns></returns>
        Result<string> GetReplyEmail(Guid id);

        /// <summary>
        /// Restful-Api用
        /// 取得列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiParticipationDiscussVm> GetList();

        /// <summary>
        /// Restful-Api用
        /// 取得討論區留言之欄位資料內容
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiParticipationDiscussDetailVm> GetDetail(Guid id,string keyword);

        #endregion New


        #region 後台

        /// <summary>
        /// 公民論壇-單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        ParticipationDataVm GetParticipationDetail(Guid id);

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">id:公民論壇編號</param>
        /// <returns></returns>
        IQueryable<ParticipationDiscussVm> GetList(SearchVm key);

        /// <summary>
        /// 置頂/非置頂
        /// </summary>
        /// <param name="id">discussId</param>
        /// <returns></returns>
        Result<string> GetUpdateTop(Guid id);

        #endregion
    }
}