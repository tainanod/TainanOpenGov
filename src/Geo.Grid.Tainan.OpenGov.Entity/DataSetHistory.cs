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
    /// 野生台南-資料目錄-歷程管理
    /// </summary>
    [Table("DataSetHistory")]
    public class DataSetHistory : AuditableEntityNew
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid HistoryId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// setId
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [StringLength(255)]
        [DisplayName("標題")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 使用指南
        /// </summary>
        [StringLength(8000)]
        [DisplayName("使用指南")]
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        [StringLength(8000)]
        [DisplayName("簡介")]
        public string Info { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 野生台南-資料目錄
        /// </summary>
        public virtual DataSet DataSet { get; set; }
    }
}
