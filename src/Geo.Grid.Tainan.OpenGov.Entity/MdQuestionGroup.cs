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
    /// 問題群組
    /// </summary>
    [Table("MdQuestionGroup")]
    public class MdQuestionGroup
    {
        /// <summary>
        /// 建構式
        /// </summary>
        public MdQuestionGroup()
        {
            IsEnable = true;
            EditDate = System.DateTime.Now;
            MdQuestions = new List<MdQuestion>();
        }

        /// <summary>
        /// 群組Guid
        /// </summary>
        [Key]
        public Guid GroupId { get; set; }
        /// <summary>
        /// 群組標題
        /// </summary>
        [MaxLength(500)]
        public string GroupTitle { get; set; } 
        /// <summary>
        /// 問卷Guid
        /// </summary>
        public Guid InfoId { get; set; }
        /// <summary>
        /// 群組說明
        /// </summary>
        [MaxLength(500)]
        public string GroupDesc { get; set; } 
        /// <summary>
        /// 群組順序
        /// </summary>
        public int GroupSort { get; set; } 
        /// <summary>
        /// 0刪除 1存在
        /// </summary>
        public bool IsEnable { get; set; } 
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime EditDate { get; set; }
        /// <summary>
        /// 編輯者ID
        /// </summary>
        public Guid? Editer { get; set; } 

        // Reverse navigation
        /// <summary>
        /// 群組底下的問卷問題 MdQuestion.FK_MdQuestion_MdQuestionGroup
        /// </summary>
        public virtual ICollection<MdQuestion> MdQuestions { get; set; }  

        // Foreign keys
        /// <summary>
        /// 群組所屬問卷  FK_MdQuestionGroup_MdQuestInfo
        /// </summary>
        public virtual MdQuestInfo MdQuestInfo { get; set; } 
    }
}
