using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.DataSet
{
    /// <summary>
    /// 野生台南-應用展示
    /// </summary>
    public class ShowCaseVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid CaseId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 內容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 主題圖
        /// </summary>
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
