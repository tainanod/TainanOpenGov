using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    /// <summary>
    /// 台南開放政府Restful Api
    /// 取得應用展示之欄位資料內容
    /// </summary>
    public class ApiShowCaseVm
    {
        /// <summary>
        /// 成果名稱
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 創造者
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 應用簡介
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 應用資料集
        /// </summary>
        public IEnumerable<ShowCase.PageNameVm> DataSetList { get; set; }
    }
}
