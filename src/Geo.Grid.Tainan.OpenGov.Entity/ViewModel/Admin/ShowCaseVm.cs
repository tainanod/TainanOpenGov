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
    /// 野生台南-應用展示
    /// </summary>
    public class ShowCaseVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid CaseId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [DisplayName("標題")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 創作者
        /// </summary>
        [DisplayName("創作者")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 連絡E-Mail
        /// </summary>
        [DisplayName("連絡E-Mail")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// 簡介
        /// </summary>
        [UIHint("Info")]
        [DisplayName("簡介")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 主題圖
        /// </summary>
        [UIHint("Photo")]
        [DisplayName("主題圖")]
        public Guid PhotoId { get; set; }

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
        /// 多圖管理
        /// </summary>
        [UIHint("PhotoList")]
        [DisplayName("多圖管理")]
        public IEnumerable<string> PhotoList { get; set; }

        /// <summary>
        /// 資料目錄
        /// </summary>
        [UIHint("SelectList_Chosen")]
        [DisplayName("資料目錄")]
        public IEnumerable<Guid> DataSetList { get; set; }

        /// <summary>
        /// 相關連結
        /// </summary>
        [UIHint("PageUrl")]
        [DisplayName("相關連結")]
        public IEnumerable<PageNameVm> UrlList { get; set; }

        ///// <summary>
        ///// 野生台南-應用展示-連結網址
        ///// </summary>
        //public virtual ICollection<ShowCaseUrl> ShowCaseUrl { get; set; }

        ///// <summary>
        ///// 野生台南-資料目錄
        ///// </summary>
        //public virtual ICollection<DataSet> DataSetRel { get; set; }

        ///// <summary>
        ///// 圖片管理
        ///// </summary>
        //public virtual ICollection<Photo> PhotoRel { get; set; }
    }
}
