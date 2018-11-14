using System;
using System.Collections.Generic;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IAllowanceService
    {
        #region 補助

        /// <summary>
        /// 關鍵字搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>補助 資料</returns>
        PageResult<AllowanceVm> Query(AllowanceQueryVm queryVm);

        /// <summary>
        /// 進階搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>補助 資料</returns>
        PageResult<AllowanceVm> AdvQuery(AllowanceQueryVm queryVm);

        /// <summary>
        /// 取得 明細
        /// </summary>
        /// <param name="id">補助ID</param>
        /// <returns>補助 資料</returns>
        Result<AllowanceVm> Detail(Guid id);

        /// <summary>
        /// 區公所
        /// </summary>
        /// <returns>區公所 資料</returns>
        Result<List<DistrictOffice>> GetDistrictOffice();

        /// <summary>
        /// 請問您是否具備下列其他身分條件
        /// </summary>
        /// <returns>其他身分條件</returns>
        List<string> GetOthers();

        /// <summary>
        /// 請問您是否具備下列特殊身分條件?
        /// </summary>
        /// <returns>特殊身分條件</returns>
        List<string> GetIdentity();

        #endregion

        #region 資料集來源管理

        /// <summary>
        /// 取得 資料集來源管理
        /// </summary>
        /// <returns></returns>
        IQueryable<AllowanceSource> GetAllowanceSourceList();

        /// <summary>
        /// 刪除 資料集來源管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<AllowanceSource> RemoveAllowanceSource(Guid id);

        /// <summary>
        /// 新增 資料集來源管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<AllowanceSource> CreateAllowanceSource(AllowanceSource model);

        /// <summary>
        /// 取得 資料集來源管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AllowanceSource GetAllowanceSource(Guid id);

        /// <summary>
        /// 更新 資料集來源管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<AllowanceSource> UpdateAllowanceSource(AllowanceSource model);

        /// <summary>
        /// 更新 補助
        /// </summary>
        /// <returns></returns>
        Result<string> RefreshAllowanceList();

        /// <summary>
        /// 更新一筆 資料集來源
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        Result<string> RefreshAllowanceBySourceId(Guid sourceId);

        #endregion
    }
}
