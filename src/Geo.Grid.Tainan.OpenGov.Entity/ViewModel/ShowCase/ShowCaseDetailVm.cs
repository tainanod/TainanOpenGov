using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.ShowCase
{
    /// <summary>
    /// 野生台南-應用展示
    /// </summary>
    public class ShowCaseDetailVm
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
        /// 創作者
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 主題圖
        /// </summary>
        public string PhotoUrl { get; set; } = string.Empty;

        /// <summary>
        /// 多圖管理
        /// </summary>
        public IEnumerable<PageNameVm> PhotoList { get; set; }

        /// <summary>
        /// 資料目錄
        /// </summary>
        public IEnumerable<PageNameVm> DataSetList { get; set; }

        /// <summary>
        /// 相關連結
        /// </summary>
        public IEnumerable<PageNameVm> UrlList { get; set; }
    }
}
