using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    public class CityTown
    {
        /// <summary>
        /// CityTownId
        /// </summary>
        [Key]
        public Guid CityTownId { get; set; }
        /// <summary>
        /// 鄉鎮區 郵遞區號
        /// </summary>
        public int TownSeq { get; set; }
        /// <summary>
        /// 鄉鎮區名稱
        /// </summary>
        [MaxLength(50)]
        public string TownName { get; set; }
        /// <summary>
        /// 縣市序號
        /// </summary>
        public int CitySeq { get; set; }
        /// <summary>
        /// 縣市名稱
        /// </summary> 
        [MaxLength(50)]
        public string CityName { get; set; }
        /// <summary>
        /// 是否啟用
        /// </summary> 
        public bool IsEnable { get; set; }
    }
}
