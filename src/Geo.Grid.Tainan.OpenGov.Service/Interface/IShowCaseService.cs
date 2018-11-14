using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.ShowCase;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Common;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    /// <summary>
    /// 野生台南-應用展示
    /// </summary>
    public interface IShowCaseService
    {
        /// <summary>
        /// Restful-Api用
        /// 取得列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiShowCaseVm> GetList();

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">search</param>
        /// <returns></returns>
        PageResult<ShowCaseVm> GetList(SearchVm key);

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">guid</param>
        /// <returns></returns>
        ShowCaseDetailVm GetDetail(Guid id);

        /// <summary>
        /// 資料目錄
        /// </summary>
        /// <returns></returns>
        IQueryable<PageNameVm> GetDataSetList();

        /// <summary>
        /// 投稿-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetCreate(ShowCaseCreateVm vmD);

        /// <summary>
        /// 使用者資料
        /// </summary>
        /// <returns></returns>
        UserVm GetUserDetail();

    }
}
