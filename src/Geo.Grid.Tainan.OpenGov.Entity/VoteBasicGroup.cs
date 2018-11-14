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
    /// 投票管理-基本資料-群組
    /// </summary>
    [Table("VoteBasicGroup")]
    public class VoteBasicGroup
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public int GroupId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [StringLength(50)]
        [DisplayName("標題")]
        public string Name { get; set; } = string.Empty;

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
        /// 投票管理-基本資料-細項
        /// </summary>
        public virtual ICollection<VoteBasic> VoteBasic { get; set; }

        /// <summary>
        /// 投票管理-主表
        /// </summary>
        public virtual ICollection<Vote> Votes { get; set; }

    }
}
