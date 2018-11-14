using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 野生台南-應用展示
    /// </summary>
    [Table("ShowCase")]
    public class ShowCase : AuditableEntityNew
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid CaseId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [StringLength(400)]
        [DisplayName("標題")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 創作者
        /// </summary>
        [StringLength(255)]
        [DisplayName("創作者")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 連絡Email
        /// </summary>
        [StringLength(128)]
        [DisplayName("連絡Email")]
        public string UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        [StringLength(8000)]
        [DisplayName("簡介")]
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 主題圖
        /// </summary>
        [DisplayName("主題圖")]
        public Guid PhotoId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否刪除
        /// </summary>
        [DisplayName("是否刪除")]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 野生台南-應用展示-連結網址
        /// </summary>
        public virtual ICollection<ShowCaseUrl> ShowCaseUrl { get; set; }

        /// <summary>
        /// 野生台南-資料目錄
        /// </summary>
        public virtual ICollection<DataSet> DataSetRel { get; set; }

        /// <summary>
        /// 圖片管理
        /// </summary>
        public virtual ICollection<Photo> PhotoRel { get; set; }
    }
}
