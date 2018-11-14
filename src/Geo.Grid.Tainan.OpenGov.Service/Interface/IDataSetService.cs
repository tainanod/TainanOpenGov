using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.DataSet;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    /// <summary>
    /// 資料目錄
    /// </summary>
    public interface IDataSetService
    {
        /// <summary>
        /// Restful-Api用
        /// 取得列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiDataSetVm> GetList();

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        PageResult<DataSetVm> GetList(SearchVm key);

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        DataSetDetailVm GetDetail(Guid id);

        /// <summary>
        /// Restful-Api用
        /// 取得單一資料目錄之欄位資料內容
        /// </summary>
        /// <param name="id">資料目錄編號</param>
        /// <returns></returns>
        ApiDataSetDetailVm GetDataSetDetail(Guid id);

        /// <summary>
        /// 類別
        /// </summary>
        /// <returns></returns>
        IQueryable<DataSetTypeVm> GetTypeList();
    }
}
