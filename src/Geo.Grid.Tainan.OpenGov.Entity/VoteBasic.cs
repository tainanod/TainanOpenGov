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
    /// 投票管理-基本資料-細項
    /// </summary>
    [Table("VoteBasic")]
    public class VoteBasic
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid BasicId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// key-group
        /// </summary>
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
        /// 是否需要填文字
        /// </summary>
        [DisplayName("是否需要填文字")]
        public bool IsExplain { get; set; } = false;

        /// <summary>
        /// 投票管理-基本資料-群組
        /// </summary>
        public virtual VoteBasicGroup VoteBasicGroup { get; set; }
    }
}
