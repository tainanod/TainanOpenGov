using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.DataSet
{
    /// <summary>
    /// 資料目錄-列表
    /// </summary>
    public class DataSetVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid Setid { get; set; }

        /// <summary>
        /// 類別-編號
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 類別-名稱
        /// </summary>
        public string TypeName { get; set; } = string.Empty;

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        public string Info { get; set; } = string.Empty;

        /// <summary>
        /// 資料格式
        /// </summary>
        public IEnumerable<PageNameVm> ExtensionList { get; set; }
    }
}
