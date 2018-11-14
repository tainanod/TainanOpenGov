using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
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
        public string Title { get; set; }

        /// <summary>
        /// 網址
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// 連結方式
        /// </summary>
        public bool Target { get; set; }

        /// <summary>
        /// 圖片管理
        /// </summary>
        public string PhotoUrl { get; set; }
    }
}
