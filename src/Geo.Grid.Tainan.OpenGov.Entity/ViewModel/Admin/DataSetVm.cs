using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// 野生台南-資料目錄
    /// </summary>
    public class DataSetVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid SetId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 類別
        /// </summary>
        [Required]
        [UIHint("SelectList")]
        [DisplayName("類別")]
        public Guid TypeId { get; set; }

        /// <summary>
        /// 類別
        /// </summary>
        [DisplayName("類別")]
        public string TypeName { get; set; } = string.Empty;

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [DisplayName("標題")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 業管單位
        /// </summary>
        [UIHint("SelectList")]
        [DisplayName("業管單位")]
        public Guid UnitId { get; set; }

        /// <summary>
        /// 業管單位
        /// </summary>
        [DisplayName("業管單位")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UnitName { get; set; } = string.Empty;

        /// <summary>
        /// 聯繫窗口
        /// </summary>
        [DisplayName("聯繫窗口")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ContactName { get; set; } = string.Empty;

        /// <summary>
        /// 聯絡電話
        /// </summary>
        [DisplayName("聯絡電話")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ContactTel { get; set; } = string.Empty;

        /// <summary>
        /// 連結地址
        /// </summary>
        [DisplayName("連結地址")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string WebUrl { get; set; } = string.Empty;

        /// <summary>
        /// 最新版次
        /// </summary>
        [DisplayName("聯絡信箱")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ContactEmail { get; set; } = string.Empty;

        /// <summary>
        /// 使用指南
        /// </summary>
        [UIHint("EditorMd")]
        [DisplayName("使用指南")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        [UIHint("Info")]
        [DisplayName("簡介")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Info { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

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
        /// 資料類型
        /// </summary>
        [UIHint("SelectList_Chosen")]
        [DisplayName("資料類型")]
        public IEnumerable<Guid> ExtensionList { get; set; }
    }
}
