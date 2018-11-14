using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{
    /// <summary>
    /// 台南開放政府Restful Api
    /// 取得討論區留言之欄位資料內容
    /// </summary>
    public class ApiParticipationDiscussDetailVm
    {
        /// <summary>
        /// 討論區編碼
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 論壇編碼
        /// </summary>
        public Guid ParticipationId { get; set; }
        /// <summary>
        /// 論壇主題
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 發布時間
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 留言內容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 推文數量
        /// </summary>
        public int UserPush { get; set; }
        /// <summary>
        /// 標籤內容
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
    }
}
