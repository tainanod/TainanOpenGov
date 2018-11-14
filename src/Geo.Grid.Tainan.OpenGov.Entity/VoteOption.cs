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
    /// 投票管理-選項
    /// </summary>
    [Table("VoteOption")]
    public class VoteOption
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid OptionId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Vote-key
        /// </summary>
        [DisplayName("Vote-key")]
        public Guid VoteId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [StringLength(255)]
        [DisplayName("標題")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; } = 0;

        /// <summary>
        /// 是否顯示
        /// </summary>
        [DisplayName("是否顯示")]
        public bool IsEnabled { get; set; } = true;

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
        /// 投票管理-主表
        /// </summary>
        public virtual Vote Vote { get; set; }
    }
}
