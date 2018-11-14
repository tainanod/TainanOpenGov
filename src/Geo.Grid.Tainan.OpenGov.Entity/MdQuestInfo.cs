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
    /// 問卷資訊
    /// </summary>
    [Table("MdQuestInfo")]
    public class MdQuestInfo
    {
        /// <summary>
        /// 建構式
        /// </summary>
        public MdQuestInfo()
        {
            InfoTitle = "";
            InfoDesc = "";
            InfoValid = true;
            InfoFooter = "";
            EditDate = System.DateTime.Now;
            IsEnable = true;
            MdQuestionGroups = new List<MdQuestionGroup>();
        }

        /// <summary>
        /// 問卷Guid
        /// </summary>
        [Key]
        public Guid InfoId { get; set; }

        /// <summary>
        /// 所屬的開放論壇ID
        /// </summary>
        public Guid ForumId { get; set; }

        /// <summary>
        /// 問卷標題
        /// </summary>
        [MaxLength(500)]
        public string InfoTitle { get; set; }

        /// <summary>
        /// 問卷描述
        /// </summary>
        [MaxLength(3000)]
        public string InfoDesc { get; set; }

        /// <summary>
        /// 開放填寫日期(起)
        /// </summary>
        public DateTime? InfoDateSt { get; set; }

        /// <summary>
        /// 開放填寫日期(迄)
        /// </summary>
        public DateTime? InfoDateEnd { get; set; }

        /// <summary>
        /// 是否開放 1:使用  0:停用
        /// </summary>
        public bool InfoValid { get; set; }

        /// <summary>
        /// 問卷結語
        /// </summary>
        [MaxLength(500)]
        public string InfoFooter { get; set; }

        /// <summary>
        /// 更新時間
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
        /// 問卷結果是否開放(統計表)
        /// </summary>
        public bool IsGather { get; set; } = false;

        /// <summary>
        /// 問題群組 MdQuestionGroup.FK_MdQuestionGroup_MdQuestInfo 
        /// </summary>
        public virtual ICollection<MdQuestionGroup> MdQuestionGroups { get; set; }

        public virtual Forum Forum { get; set; }

        public int VerifyType { get; set; } = 0;

    }
}
