using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 投票管理-主表
    /// </summary>
    [Table("Vote")]
    public class Vote
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid VoteId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// forum-key
        /// </summary>
        [ForeignKey("Forum")]
        public Guid ForumId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>        
        [Required]
        [StringLength(255)]
        [DisplayName("標題")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 內容
        /// </summary>
        [StringLength(8000)]
        [DisplayName("內容")]
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
        /// 是否刪除
        /// </summary>
        [DisplayName("是否刪除")]
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// 建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 建立人員
        /// </summary>
        [StringLength(128)]
        [DisplayName("建立人員")]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 修改時間
        /// </summary>
        [DisplayName("修改時間")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人員
        /// </summary>
        [StringLength(128)]
        [DisplayName("修改人員")]
        public string UpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 驗證方式
        /// </summary>
        [DisplayName("驗證方式")]
        public int VerifyType { get; set; }

        /// <summary>
        /// 投票管理-選項
        /// </summary>
        public virtual ICollection<VoteOption> VoteOption { get; set; }

        /// <summary>
        /// 投票管理-基本資料-群組
        /// </summary>
        public virtual ICollection<VoteBasicGroup> VoteBasicGroups { get; set; }

        /// <summary>
        /// 公民論壇
        /// </summary>
        public virtual Forum Forum { get; set; }
    }
}
