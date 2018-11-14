using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// md圖片上傳
    /// </summary>
    public class MdUploadVm
    {
        /// <summary>
        /// 1:true ; 0:false
        /// </summary>
        public int success { get; set; }

        /// <summary>
        /// message
        /// </summary>
        public string message { get; set; } = string.Empty;

        /// <summary>
        /// url
        /// </summary>
        public string url { get; set; } = string.Empty;
    }
}
