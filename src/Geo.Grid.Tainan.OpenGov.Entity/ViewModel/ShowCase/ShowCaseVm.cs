using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.ShowCase
{
    /// <summary>
    /// 野生台南-應用展示
    /// </summary>
    public class ShowCaseVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid CaseId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 主題圖
        /// </summary>
        public string PhotoUrl { get; set; } = string.Empty;

    }
}
