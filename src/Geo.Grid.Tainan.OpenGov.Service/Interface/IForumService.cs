using System;
using System.Collections.Generic;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Newtonsoft.Json.Linq;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IForumService
    {
        IQueryable<Forum> QueryForum(ForumType type);

        IQueryable<Forum> GetForumByType(ForumType type);

        Result<Forum> Create(Forum model, bool? isSubForum = null);

        Forum GetForum(Guid id);

        Forum GetForumWithRelation(Guid id);

        IQueryable<Document> GetDocumentWithType(Guid id, DocType docType);

        Result<Hyperlink> CreateHyperlink(Guid id, Hyperlink model);

        /// <summary>
        /// Restful-Api用
        /// 取得公民論壇之清單
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiForumVm> GetList();

        /// <summary>
        /// Restful-Api用
        /// 取得單一公民論壇之欄位資料內容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiForumDetailVm GetDetail(Guid id);

        IQueryable<Hyperlink> GetHyperlink(Guid id);

        Result<Forum> UpdateForum(Forum model, bool? isSubForum = null);

        Result<Forum> RemoveForum(Guid id);

        IQueryable<Discuss> GetDiscuss(Guid id);

        Result<JObject> GetForumDiscuss(Guid id, DateTime startDate, DateTime endDate);

        IQueryable<Forum> GetSubForums(Guid id);

        Result<ForumVm> GetForumAndRelations(Guid forumId);
    }
}