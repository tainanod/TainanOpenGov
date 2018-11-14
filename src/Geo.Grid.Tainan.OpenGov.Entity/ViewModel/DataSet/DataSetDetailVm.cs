using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.DataSet
{
    /// <summary>
    /// 野生台南-資料目錄-內容頁
    /// </summary>
    public class DataSetDetailVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 業管單位
        /// </summary>
        public string UntiName { get; set; } = string.Empty;

        /// <summary>
        /// 聯繫窗口
        /// </summary>
        public string ContactName { get; set; } = string.Empty;

        /// <summary>
        /// 聯絡電話
        /// </summary>
        public string ContactTel { get; set; } = string.Empty;

        /// <summary>
        /// 連結位址
        /// </summary>
        public string WebUrl { get; set; } = string.Empty;

        /// <summary>
        /// 使用指南-md
        /// </summary>
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        public string Info { get; set; } = string.Empty;

        /// <summary>
        /// 版本編號v1.0
        /// </summary>
        public string VersionName { get; set; } = string.Empty;

        /// <summary>
        /// 格式
        /// </summary>
        public string ExtensionArray { get; set; } = string.Empty;

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// 歷程管理
        /// </summary>
        public IEnumerable<DataSetHistoryVm> DataSetHistoryList { get; set; }

        /// <summary>
        /// 應用展示
        /// </summary>
        public IEnumerable<ShowCaseVm> ShowCaseList { get; set; }
    }
}
