using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 查詢
    /// </summary>
    public class SearchVm
    {
        /// <summary>
        /// 關鍵字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// 分頁
        /// </summary>
        public int Page { get; set; } = 1;

    }
}
