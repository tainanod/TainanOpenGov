using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 公民論壇-留言版-搜尋
    /// </summary>
    public class DiscussSearchVm
    {
        /// <summary>
        /// 公民論壇key
        /// </summary>
        public Guid ForumId { get; set; }

        /// <summary>
        /// 關鍵字
        /// </summary>
        public string KeyWord { get; set; } = string.Empty; 

        /// <summary>
        /// 標籤
        /// </summary>
        public Guid? TagId { get; set; } 

        /// <summary>
        /// 排序
        /// 1:依發言時間
        /// 2:依推文時間
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 分頁
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// 分頁數
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
