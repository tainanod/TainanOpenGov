using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// 野生台南-資料目錄-歷程管理
    /// </summary>
    public class DataSetHistoryVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid HistoryId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// setId
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [UIHint("Label")]
        [DisplayName("版號")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 使用指南
        /// </summary>
        [UIHint("EditorMd")]
        [DisplayName("使用指南")]
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        [UIHint("Info")]
        [DisplayName("歷程說明")]
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

    }
}
