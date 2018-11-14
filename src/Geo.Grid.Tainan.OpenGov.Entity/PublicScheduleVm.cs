using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    public class PublicScheduleVm
    {
        public string 行程事由 { get; set; }
        
        public string 標題 { get; set; }

        public string 主辦單位 { get; set; }
        
        public string 坐標X { get; set; }
        
        public string 坐標Y { get; set; }
        
        public string 行程地點 { get; set; }

        public string 開始時間 { get; set; }
        
        public DateTime? 開始日期 { get; set; }
        public string 結束時間 { get; set; }
        
        public DateTime? 結束日期 { get; set; }

        public int _id { get; set; }

        //新 API未提供
        //public DateTime 日期 { get; set; }
    }
}
