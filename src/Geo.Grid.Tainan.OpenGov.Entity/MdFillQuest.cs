using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 使用者問卷填寫
    /// </summary>
    [Table("MdFillQuest")]
    public partial class MdFillQuest
    {
        /// <summary>
        /// 建構式
        /// </summary>
        public MdFillQuest()
        {
            FillScore = 0;
            EditDate = System.DateTime.Now;
        }

        /// <summary>
        /// 問卷填寫Guid
        /// </summary>
        [Key]
        public Guid FillId { get; set; }

        /// <summary>
        /// 問卷Guid
        /// </summary>
        public Guid InfoId { get; set; }

        /// <summary>
        /// 問卷填寫人Guid
        /// </summary>
        public Guid MdFillUserId { get; set; }

        /// <summary>
        /// 問卷填寫人eMail
        /// </summary>
        [StringLength(128)]
        public string MdFillUserEmail { get; set; } = string.Empty;

        /// <summary>
        /// 題目Guid
        /// </summary>
        public Guid QstId { get; set; }

        /// <summary>
        /// 選項Guid (若題目為問答題，此欄位為null)
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// 文字描述答案
        /// </summary>
        [MaxLength(3000)]
        public string FillDesc { get; set; }

        /// <summary>
        /// 計分
        /// </summary>
        public int FillScore { get; set; }

        /// <summary>
        /// 使用者更新時間
        /// </summary>
        public DateTime EditDate { get; set; }

        /// <summary>
        /// 使用者編號
        /// </summary>
        public Guid? Editer { get; set; }

        /// <summary>
        /// 是否顯示
        /// </summary>
        public bool IsEnable { get; set; } = true;

        // Foreign keys
        /// <summary>
        /// FK_MdFillQuest_MdQuestion
        /// </summary>
        public virtual MdQuestion MdQuestion { get; set; }
    }
}

