using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface.Admin
{
    /// <summary>
    /// 投票管理
    /// </summary>
    public interface IVoteService
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
        IQueryable<VoteVm> GetList(SearchVm key);

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetCreate(VoteVm vmD);

        /// <summary>
        /// 編輯-單筆
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        VoteVm GetDetail(Guid id);

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetEdit(VoteVm vmD);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        Result<string> GetRemove(Guid id);

        #region 統計圖表

        /// <summary>
        /// 圖表
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        IQueryable<VoteGatherVm> GetGather(Guid id);

        /// <summary>
        /// 圖片-個資
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        IQueryable<VoteGatherVm> GetGatherBasic(Guid id);

        #endregion

        #region 匯出

        /// <summary>
        /// 匯出
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        Result<byte[]> GetExcel(Guid id);

        #endregion

        #region 共用

        /// <summary>
        /// 個資-分類
        /// </summary>
        /// <returns></returns>
        IQueryable<IdNameVm> GetGroup();

        /// <summary>
        /// 選項
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        IQueryable<GuidNameVm> GetOption(Guid id);

        /// <summary>
        /// 驗證方式
        /// </summary>
        /// <returns></returns>
        IEnumerable<IdNameVm> GetVerifyType();

        #endregion
    }
}
