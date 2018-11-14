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
    [Table("ShowCaseUrl")]
    public class ShowCaseUrl : AuditableEntityNew
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid UrlId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 野生台南-資料目錄
        /// </summary>
        public Guid CaseId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [StringLength(400)]
        [DisplayName("標題")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 網址
        /// </summary>
        [StringLength(800)]
        [DisplayName("網址")]
        public string WebUrl { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 野生台南-應用展示
        /// </summary>
        public virtual ShowCase ShowCase { get; set; }
    }
}
