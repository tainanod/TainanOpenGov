using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// 公民論壇-討論區
    /// </summary>
    public class DiscussVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid DiscussId { get; set; }

        /// <summary>
        /// 公民論壇id
        /// </summary>
        public Guid ForumId { get; set; }

        /// <summary>
        /// 留言者
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 留言者-匿稱
        /// </summary>
        public string UserNickName { get; set; } = string.Empty;

        /// <summary>
        /// null:主題；no null:子論壇
        /// </summary>
        public Guid? UpperId { get; set; }

        /// <summary>
        /// 留言內容
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 是否顯示
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 是否置頂
        /// </summary>
        public bool IsTop { get; set; } = false;

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
