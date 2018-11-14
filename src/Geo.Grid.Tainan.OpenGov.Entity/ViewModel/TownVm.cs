using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 鄉鎮市區資料ViewModel
    /// </summary>
    public class TownVm
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid CityTownId { get; set; }

        /// <summary>
        /// 鄉鎮市區郵遞區號
        /// </summary>
        public int TownSeq { get; set; }

        /// <summary>
        /// 鄉鎮市區名稱
        /// </summary>
        public string TownName { get; set; }
    }
}
