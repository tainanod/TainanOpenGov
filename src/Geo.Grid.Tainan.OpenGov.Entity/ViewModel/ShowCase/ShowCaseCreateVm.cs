using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.ShowCase
{
    /// <summary>
    /// 野生台南-應用展示-post
    /// </summary>
    public class ShowCaseCreateVm
    {
        /// <summary>
        /// 標題
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 創作者
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 聯絡email
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Info { get; set; } = string.Empty;

        /// <summary>
        /// 網址連結
        /// </summary>
        public IEnumerable<ShwoCaseCreateLinkVm> LinkArray { get; set; } = null;

        /// <summary>
        /// 野生台南-資料目錄
        /// </summary>
        public IEnumerable<PageNameVm> TagArray { get; set; } = null;

        /// <summary>
        /// 主題圖
        /// </summary>
        public HttpPostedFileBase AppImg { get; set; }

        /// <summary>
        /// 圖片
        /// </summary>
        public HttpPostedFileBase[] AppImgArray { get; set; } = null;
    }

    /// <summary>
    /// 網站連結
    /// </summary>
    public class ShwoCaseCreateLinkVm
    {
        /// <summary>
        /// 標題
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 網址
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Content { get; set; } = string.Empty;
    }
}
