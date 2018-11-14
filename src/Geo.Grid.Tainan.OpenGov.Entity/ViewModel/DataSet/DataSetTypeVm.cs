using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.DataSet
{
    /// <summary>
    /// 資料目錄-類別
    /// </summary>
    public class DataSetTypeVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 數量
        /// </summary>
        public int Counts { get; set; }
    }
}
