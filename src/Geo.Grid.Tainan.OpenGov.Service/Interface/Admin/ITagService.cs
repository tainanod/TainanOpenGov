using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface.Admin
{
    /// <summary>
    /// 標籤管理 
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// 公民論壇-單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        ForumVm GetForumDetail(Guid id);

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        IQueryable<TagVm> GetList(SearchVm key);

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetCreate(TagVm vmD);

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        TagVm GetDetail(Guid id);

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetEdit(TagVm vmD);

        /// <summary>
        /// 刪除-回傳forumId
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        Result<string> GetRemove(Guid id);

        /// <summary>
        /// 排序-升
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        Result<string> GetSortUp(Guid id);

        /// <summary>
        /// 排序降
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        Result<string> GetSortDown(Guid id);
    }
}
