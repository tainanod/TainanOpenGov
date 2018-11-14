using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// 投票管理-主表
    /// </summary>
    public class VoteVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid VoteId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 標題
        /// </summary>
        [DisplayName("標題")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 內容
        /// </summary>
        [DisplayName("內容")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Info { get; set; } = string.Empty;

        /// <summary>
        /// 投票開始日
        /// </summary>
        [DisplayName("投票開始日")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        /// <summary>
        /// 投票結束日
        /// </summary>
        [DisplayName("投票結束日")]
        public DateTime EndDate { get; set; } = DateTime.Today.AddMonths(3);

        /// <summary>
        /// 結果是否開放
        /// </summary>
        [DisplayName("結果是否開放")]
        public bool CanVote { get; set; } = false;

        /// <summary>
        /// 選項最多勾選幾個
        /// </summary>
        [DisplayName("選項最多勾選幾個")]
        public int SelectNumber { get; set; } = 2;

        /// <summary>
        /// 是否發布
        /// </summary>
        [DisplayName("是否發布")]
        public bool IsPublish { get; set; } = true;

        /// <summary>
        /// 建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 驗證方式
        /// </summary>
        [DisplayName("驗證方式")]
        public int VerifyType { get; set; }

        /// <summary>
        /// 公民論壇ID
        /// </summary>
        public Guid ForumId { get; set; }

        /// <summary>
        /// 資料填寫欄位
        /// </summary>
        public IEnumerable<int> GroupIdArray { get; set; }

        /// <summary>
        /// 選項-多筆
        /// </summary>
        public List<GuidNameVm> OptionArray { get; set; }
    }
}
