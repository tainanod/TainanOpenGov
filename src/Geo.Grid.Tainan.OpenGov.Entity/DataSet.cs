using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 野生台南-資料目錄
    /// </summary>
    [Table("DataSet")]
    public class DataSet : AuditableEntityNew
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid SetId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 類別
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [StringLength(255)]
        [DisplayName("標題")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 業管單位
        /// </summary>
        [DisplayName("業管單位")]
        public Guid UnitId { get; set; }

        /// <summary>
        /// 聯繫窗口
        /// </summary>
        [StringLength(255)]
        [DisplayName("聯繫窗口")]
        public string ContactName { get; set; } = string.Empty;

        /// <summary>
        /// 聯絡電話
        /// </summary>
        [StringLength(255)]
        [DisplayName("聯絡電話")]
        public string ContactTel { get; set; } = string.Empty;

        /// <summary>
        /// 連結地址
        /// </summary>
        [StringLength(400)]
        [DisplayName("連結地址")]
        public string WebUrl { get; set; } = string.Empty;

        /// <summary>
        /// 最新版次
        /// </summary>
        [StringLength(800)]
        [DisplayName("聯絡信箱")]
        public string ContactEmail { get; set; } = string.Empty;

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
        /// 是否刪除
        /// </summary>
        [DisplayName("是否刪除")]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 野生台南-資料目錄-類別
        /// </summary>
        public virtual DataSetType DataSetType { get; set; }

        /// <summary>
        /// 野生台南-資料目錄-格式
        /// </summary>
        public virtual ICollection<DataSetExtension> DataSetExtensionRel { get; set; }

        /// <summary>
        /// 野生台南-資料目錄-歷程管理
        /// </summary>
        public virtual ICollection<DataSetHistory> DataSetHistory { get; set; }

        /// <summary>
        /// 野生台南-應用展示
        /// </summary>
        public virtual ICollection<ShowCase> ShowCaseRel { get; set; }

        /// <summary>
        /// 野生台南-業管單位
        /// </summary>
        public virtual DataSetUnit DataSetUnit { get; set; }
    }
}
