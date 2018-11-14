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
    /// 野生台南-資料目錄-類別
    /// </summary>
    public interface IDataSetService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        IQueryable<DataSetVm> GetList(SearchVm key);

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetCreate(DataSetVm vmD);

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        DataSetVm GetDetail(Guid id);

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetEdit(DataSetVm vmD);

        /// <summary>
        /// 刪除-儲存
        /// </summary>
        /// <param name="id">參數</param>
        /// <returns></returns>
        Result<string> GetRemove(Guid id);

        /// <summary>
        /// 類別
        /// </summary>
        /// <returns></returns>
        IQueryable<GuidNameVm> GetTypeList();

        /// <summary>
        /// 資料類型
        /// </summary>
        /// <returns></returns>
        IQueryable<GuidNameVm> GetExtensionList();

        /// <summary>
        /// 業管單位
        /// </summary>
        /// <returns></returns>
        IQueryable<GuidNameVm> GetUnitList();
    }
}
