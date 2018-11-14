using System;
using System.Collections.Generic;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    /// <summary>
    /// 台南開放政府Restful Api
    /// 取得單一公民論壇之欄位資料內容
    /// </summary>
    public class ApiParticipationDetailVm
    {
        /// <summary>
        /// 論壇編碼
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 主題類別
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 論壇主題
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 主辦單位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 討論區公告
        /// </summary>
        public string Announcement { get; set; }
        /// <summary>
        /// 發佈時間
        /// </summary>
        public DateTime OpenDate { get; set; }
        /// <summary>
        /// 截止時間
        /// </summary>
        public DateTime CloseDate { get; set; }
        ///<summary>
        /// 是否開放討論區
        ///</summary>
        public bool EnableDiscuss { get; set; }
        /// <summary>
        /// 附件連結
        /// </summary>
        public IEnumerable<PageNameVm> Documents { get; set; }
        /// <summary>
        /// 相關連結
        /// </summary>
        public IEnumerable<HyperlinkVm> Links { get; set; }
        /// <summary>
        /// 市府回應連結
        /// </summary>
        public IEnumerable<PageNameVm> Replies { get; set; }
    }
}