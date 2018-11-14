using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    /// <summary>
    /// 投票管理
    /// </summary>
    public interface IVoteService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="id">公民論壇編號</param>
        /// <returns></returns>
        IQueryable<VoteVm> GetList(Guid id);

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        VoteVm GetDetail(Guid id);

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetCreate(VoteSaveVm vmD);

        /// <summary>
        /// 回傳Email驗證是否為有效
        /// </summary>
        /// <param name="voteId">voteId</param>
        /// <returns></returns>
        Result<string> GetVerifyCheckEmailTwo(Guid voteId);

        #region 統計圖表-後台

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

        /// <summary>
        /// 圖片-個資
        /// </summary>
        /// <param name="id">voteId</param>
        /// <param name="groupId">groupId</param>
        /// <returns></returns>
        IQueryable<VoteGatherVm> GetGatherBasicDetail(Guid id, int groupId);

        #endregion

        #region 共用

        /// <summary>
        /// 檢查-單筆
        /// </summary>
        /// <param name="id">voteId</param>
        /// <param name="userMail">userMail</param>
        /// <returns></returns>
        Result<string> GetDetailCheck(Guid id, string userMail);

        /// <summary>
        /// 縣市
        /// </summary>
        /// <returns></returns>
        IQueryable<SeqNameVm> GetCity();

        /// <summary>
        /// 鄉鎮區
        /// </summary>
        /// <param name="id">cityId</param>
        /// <returns></returns>
        IQueryable<CityTownVm> GetTown(int id);

        /// <summary>
        /// 縣市/鄉鎮區
        /// </summary>
        /// <returns></returns>
        IQueryable<CityTownVm> GetCityTown();

        #endregion

    }
}
