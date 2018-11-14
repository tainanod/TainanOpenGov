using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class TnodMayor
    {
        public string 坐標X { get; set; }

        public string 坐標Y { get; set; }

        public DateTime 開始時間 { get; set; }

        public string 標題 { get; set; }

        //public string Title { get; set; }
        public string 行程事由 { get; set; }

        public string 主辦單位 { get; set; }

        public string 行程地點 { get; set; }

        public DateTime 日期 { get; set; }

        public DateTime 結束時間 { get; set; }

        public int _id { get; set; }
    }
}