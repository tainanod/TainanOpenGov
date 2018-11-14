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
    /// 形像圖管理
    /// </summary>
    public interface IBannerService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        IQueryable<BannerVm> GetList(SearchVm key);

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetCreate(BannerVm vmD);

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        BannerVm GetDetail(Guid id);

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetEdit(BannerVm vmD);

        /// <summary>
        /// 刪除-儲存
        /// </summary>
        /// <param name="id">參數</param>
        /// <returns></returns>
        Result<string> GetRemove(Guid id);
    }
}
