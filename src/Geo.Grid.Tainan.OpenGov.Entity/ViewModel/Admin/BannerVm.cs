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
    /// 形像圖管理
    /// </summary>
    public class BannerVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid BannerId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        [DisplayName("標題")]
        public string Title { get; set; }

        /// <summary>
        /// 網址
        /// </summary>
        [DisplayName("連結網址")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]        
        public string WebUrl { get; set; }

        /// <summary>
        /// 連結方式
        /// </summary>
        [DisplayName("是否另開")]
        public bool Target { get; set; }

        /// <summary>
        /// 置頂
        /// </summary>
        [DisplayName("置頂")]
        public bool IsTopEnabled { get; set; }

        /// <summary>
        /// 是否顯示
        /// </summary>
        [DisplayName("是否顯示")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 圖片管理
        /// </summary>
        [UIHint("Photo")]
        [DisplayName("圖片管理")]
        public Guid PhotoId { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
