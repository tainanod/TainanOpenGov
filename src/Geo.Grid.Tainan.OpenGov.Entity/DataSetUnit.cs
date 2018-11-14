using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 野生台南-資料目錄-業管單位
    /// </summary>
    [Table("DataSetUnit")]
    public class DataSetUnit : AuditableEntityNew
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid UnitId { get; set; } = Guid.NewGuid();

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
        public int Sort { get; set; }

        /// <summary>
        /// 野生台南-資料目錄
        /// </summary>
        public virtual ICollection<DataSet> DataSet { get; set; }

        /// <summary>
        /// 討論版
        /// </summary>
        public virtual ICollection<Forum> Forum { get; set; }
    }
}
