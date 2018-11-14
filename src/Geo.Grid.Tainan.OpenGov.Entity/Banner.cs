using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 形象圖
    /// </summary>
    [Table("Banner")]
    public class Banner : AuditableEntityNew
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid BannerId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [StringLength(255)]
        [DisplayName("標題")]
        public string Title { get; set; }

        /// <summary>
        /// 網址
        /// </summary>
        [StringLength(400)]
        [DisplayName("連結網址")]
        public string WebUrl { get; set; }

        /// <summary>
        /// 連結方式
        /// </summary>
        [DisplayName("連結方式")]
        public bool Target { get; set; }

        /// <summary>
        /// 置頂
        /// </summary>
        [DisplayName("置頂")]
        public bool IsTopEnabled { get; set; }

        /// <summary>
        /// 是否刪除
        /// </summary>
        [DisplayName("是否刪除")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 圖片管理
        /// </summary>
        [DisplayName("圖片管理")]
        public Guid PhotoId { get; set; }
    }
}
