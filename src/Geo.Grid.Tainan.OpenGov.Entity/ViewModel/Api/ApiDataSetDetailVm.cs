using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    /// <summary>
    /// 台南開放政府Restful Api
    /// 取得單一資料目錄之欄位資料內容
    /// </summary>
    public class ApiDataSetDetailVm
    {
        /// <summary>
        /// 資料目錄編號
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 資料名稱
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 業管單位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 聯繫窗口
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 聯繫電化
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 連結位址
        /// </summary>
        public string WebUrl { get; set; }
        /// <summary>
        /// 資料格式
        /// </summary>
        public string ExtensionArray { get; set; }
        /// <summary>
        /// 最新版次
        /// </summary>
        public string VersionName { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdatedDate { get; set; }
        /// <summary>
        /// 簡介
        /// </summary>
        public string Info { get; set; }
    }
}
