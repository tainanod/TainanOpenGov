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
    /// 問卷問題
    /// </summary>
    [Table("MdQuestion")]
    public class MdQuestion
    {
        /// <summary>
        /// 建構式
        /// </summary>
        public MdQuestion()
        {
            QstSorting = 1;
            QstAnsType = 0;
            QstQuestion = "";
            QstScore = 0;
            EditDate = System.DateTime.Now;
            IsEnable = true;
            IsRequired = false;
            MdFillQuests = new List<MdFillQuest>();
            MdQuestSetItems = new List<MdQuestSetItem>();
        }

        /// <summary>
        /// 題目Guid
        /// </summary>
        [Key]
        public Guid QstId { get; set; }
        /// <summary>
        /// 問卷Guid
        /// </summary>
        public Guid GroupId { get; set; }
        /// <summary>
        /// 排序(題號)
        /// </summary>
        public int QstSorting { get; set; }
        /// <summary>
        /// 答案類型 1:單選(含是非) 2:複選 3:問答 4:多重問答
        /// </summary>
        public int QstAnsType { get; set; }
        /// <summary>
        /// 題目
        /// </summary>
        [MaxLength(300)]
        public string QstQuestion { get; set; }
        /// <summary>
        /// 標準分數(暫不用，未來擴充[測驗]功能使用)
        /// </summary>
        public int QstScore { get; set; }
        /// <summary>
        /// 使用者更新時間
        /// </summary>
        public DateTime EditDate { get; set; }
        /// <summary>
        /// 使用者編號
        /// </summary>
        public Guid? Editer { get; set; }
        /// <summary>
        /// 0刪除 1存在
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 0非必填 1必填
        /// </summary>
        public bool IsRequired { get; set; }

        // Reverse navigation
        /// <summary>
        /// 使用者填寫 MdFillQuest.FK_MdFillQuest_MdQuestion
        /// </summary>
        public virtual ICollection<MdFillQuest> MdFillQuests { get; set; }
        /// <summary>
        /// 問題的選項 MdQuestSetItem.FK_MdQuestSetItem_MdQuestion
        /// </summary>
        public virtual ICollection<MdQuestSetItem> MdQuestSetItems { get; set; }

        // Foreign keys
        /// <summary>
        /// 問題所屬群組 FK_MdQuestion_MdQuestionGroup
        /// </summary>
        public virtual MdQuestionGroup MdQuestionGroup { get; set; } 
    }
}
