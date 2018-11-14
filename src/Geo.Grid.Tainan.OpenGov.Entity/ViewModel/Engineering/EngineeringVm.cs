using GeoJSON.Net.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Engineering
{
    public class EngineeringVm
    {

        ///<summary>
        /// 執行機關代碼
        ///</summary>
        public string GovernmentAenciesCode { get; set; }

        ///<summary>
        /// 編號
        ///</summary>
        public string Code { get; set; }
        
        ///<summary>
        /// 標案名稱
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// 決標金額
        ///</summary>
        public decimal Amount { get; set; }
        
        ///<summary>
        /// 標案類別
        ///</summary>
        public string Category { get; set; }
        
        ///<summary>
        /// 縣市鄉鎮
        ///</summary>
        public string CityTown { get; set; }

        ///<summary>
        /// X座標、Y座標
        ///</summary>
        public System.Data.Entity.Spatial.DbGeometry Geom { get; set; }

        /// <summary>
        /// Geom to GeoJson
        /// </summary>
        public Feature GeoJson { get; set; }

        ///<summary>
        /// 詳細地點
        ///</summary>
        public string Address { get; set; }
        
        ///<summary>
        /// 差異(百分比)
        ///</summary>
        public decimal Discrepancy { get; set; }

        ///<summary>
        /// 工程進度
        ///</summary>
        public string ProgressText { get; set; }

        ///<summary>
        /// 狀態
        ///</summary>
        public string Status { get; set; }
        
        ///<summary>
        /// 實際開工日期
        ///</summary>
        public DateTime ActualStartDate { get; set; }
    }
}
