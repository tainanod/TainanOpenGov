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
    /// 問題選項
    /// </summary>
    [Table("MdQuestSetItem")]
    public class MdQuestSetItem
    {
        /// <summary>
        /// 建構式
        /// </summary>
        public MdQuestSetItem()
        {
            SetDesc = "";
            SetScore = 0;
            SetSorting = 1;
            SetNote = false;
            EditDate = System.DateTime.Now;
            IsEnable = true;
        }

        /// <summary>
        /// 選項Guid
        /// </summary>
        [Key]
        public Guid SetId { get; set; }
        /// <summary>
        /// 題目Guid
        /// </summary>
        public Guid QstId { get; set; }
        /// <summary>
        /// 項目說明
        /// </summary>
        [MaxLength(100)]
        public string SetDesc { get; set; }
        /// <summary>
        /// 分數
        /// </summary>
        public int SetScore { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SetSorting { get; set; }
        /// <summary>
        /// 是否要文字說明 1:是 0:否
        /// </summary>
        public bool SetNote { get; set; }
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

        // Foreign keys
        /// <summary>
        /// 選項所屬問題 FK_MdQuestSetItem_MdQuestion
        /// </summary>
        public virtual MdQuestion MdQuestion { get; set; }

        public virtual MdQuestNecessaryRel MdQuestNecessaryRel { get; set; }
    }
}
