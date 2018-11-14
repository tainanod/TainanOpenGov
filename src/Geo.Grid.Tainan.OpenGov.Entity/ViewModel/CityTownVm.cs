using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 縣市/鄉鎮區
    /// </summary>
    public class CityTownVm
    {
        /// <summary>
        /// 縣市-編號
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 縣市-名稱
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 鄉鎮區-編號
        /// </summary>
        public int TownId { get; set; }

        /// <summary>
        /// 鄉鎮區-名稱
        /// </summary>
        public string TownName { get; set; } = string.Empty;
    }
}
