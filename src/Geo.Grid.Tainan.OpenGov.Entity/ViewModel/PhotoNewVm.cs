using System;
using System.IO;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 圖片管理-共用
    /// </summary>
    public class PhotoNewVm
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid PhotoGuid { get; set; }

        /// <summary>
        /// stream
        /// </summary>
        public byte[] FileStream { get; set; }

        /// <summary>
        /// 檔案名稱
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 檔案類型
        /// </summary>
        public string ContentType { get; set; } = string.Empty;
    }
}
