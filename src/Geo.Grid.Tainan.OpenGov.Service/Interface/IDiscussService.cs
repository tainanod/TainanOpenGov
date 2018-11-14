using System;
using System.Collections.Generic;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IDiscussService
    {
        Result<Discuss> AddMessage(Guid id, string message, string userId);

        Result<DiscussVm> CreateMessage(Guid id, string message, string userId, List<Guid> tagIds, Guid? upperID = null);

        PageResult<DiscussVm> GetAllDiscussion(Guid id, int currentPage, int pageSize);

        Result<List<DiscussVm>> GetReply(Guid discussId);

        Result<DiscussVm> PushMessage(Guid discussId, Guid pushUserId);

        /// <summary>
        /// 標籤管理
        /// </summary>
        /// <param name="forumId">forumId</param>
        /// <returns></returns>
        IQueryable<TagVm> GetAllTags(Guid forumId);

        List<ForumVm> GetSubForum(Guid superForumId);

        #region New

        /// <summary>
        /// 取得主題下的留言-New
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        PageResult<DiscussVm> GetAllDiscussionNew(DiscussSearchVm key);

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
        IEnumerable<ApiDiscussVm> GetList();

        /// <summary>
        /// Restful-Api用
        /// 取得討論區留言之欄位資料內容
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiDiscussDetailVm> GetDetail(Guid id,string keyword);
        /// <summary>
        /// Restful-Api用
        /// 取得論壇子議題之清單內容
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiSubForumVm> GetSubforumList();
        /// <summary>
        /// Restful-Api用
        /// 取得論壇子議題之欄位資料內容
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiSubForumDetailVm> GetSubforumDetail(Guid id);

        #endregion New
    }
}