using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.DataSet
{
    /// <summary>
    /// 野生台南-資料目錄-歷程管理
    /// </summary>
    public class DataSetHistoryVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid HistoryId { get; set; }

        /// <summary>
        /// 標題(版號)
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 使用指南(md)
        /// </summary>
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 歷程說明(簡介)
        /// </summary>
        public string Info { get; set; } = string.Empty;

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
