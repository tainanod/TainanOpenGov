using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 論壇點擊用(new語法)
    /// </summary>
    [Table("AnonymousClick")]
    public class AnonymousClick
    {
        /// <summary>
        /// 匿名編號
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public string AnonymousId { get; set; } = string.Empty;

        /// <summary>
        /// tableId
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public string ClickId { get; set; } = string.Empty;

        /// <summary>
        /// A：相關連線
        /// B：文件下載
        /// C：市府回應
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public string ClickType { get; set; } = string.Empty;

        /// <summary>
        /// 論壇id
        /// </summary>
        public Guid ForumId { get; set; }

        public virtual Forum Forum { get; set; }
    }
}
