using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    /// <summary>
    /// 台南開放政府Restful Api
    /// 取得直播會議之欄位資料內容
    /// </summary>
    public class ApiYoutubeVm
    {
        /// <summary>
        /// 會議名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 直播位址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 影片描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 播放時間
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 影片長度
        /// </summary>
        public string VideoTime { get; set; }
    }
}
