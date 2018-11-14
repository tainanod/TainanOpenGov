using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Geo.Grid.Tainan.OpenGov.Entity.Interface;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// entity
    /// </summary>
    public abstract class AuditableEntityNew : IAuditableEntity
    {
        /// <summary>
        /// 是否顯示
        /// </summary>        
        [ScaffoldColumn(false)]
        [DisplayName("是否顯示")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 建立者
        /// </summary>
        [StringLength(128)]
        [ScaffoldColumn(false)]
        [DisplayName("建立者")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [ScaffoldColumn(false)]
        [DisplayName("建立時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改者
        /// </summary>
        [StringLength(128)]
        [ScaffoldColumn(false)]
        [DisplayName("修改者")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        [ScaffoldColumn(false)]
        [DisplayName("修改時間")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}