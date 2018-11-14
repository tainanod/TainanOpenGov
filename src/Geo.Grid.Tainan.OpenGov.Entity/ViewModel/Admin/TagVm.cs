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
    public class TagVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid TagId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 公民論壇id
        /// </summary>        
        public Guid ForumId { get; set; }

        /// <summary>
        /// 論壇主題
        /// </summary>
        [DisplayName("論壇主題")]
        public string ForumTitle { get; set; } = string.Empty;

        /// <summary>
        /// 標題
        /// </summary>
        [DisplayName("標題")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TagName { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否顯示
        /// </summary>
        [DisplayName("是否顯示")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
