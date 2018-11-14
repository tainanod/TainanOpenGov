using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 野生台南-資料目錄-格式
    /// </summary>
    [Table("DataSetExtension")]
    public class DataSetExtension : AuditableEntityNew
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid ExtensionId { get; set; } = Guid.NewGuid();

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
        public virtual ICollection<DataSet> DataSetRel { get; set; }

    }
}
