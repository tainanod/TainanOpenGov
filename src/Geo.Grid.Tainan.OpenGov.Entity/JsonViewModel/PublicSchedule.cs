using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Geo.Grid.Tainan.OpenGov.Entity.JsonViewModel
{
    public class PublicSchedule
    {
        [JsonProperty(PropertyName = "事由")]
        public string 行程事由 { get; set; }

        [JsonIgnore]
        public string 標題 {
            get {
                return 行程事由;
            }
        }
        
        public string 主辦單位 { get; set; }
        
        [JsonProperty(PropertyName = "longitude")]
        public string 坐標X { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public string 坐標Y { get; set; }

        [JsonProperty(PropertyName = "地點")]
        public string 行程地點 { get; set; }
        
        public string 開始時間 { get; set; }

        [JsonIgnore]
        public DateTime? 開始日期
        {
            get
            {
                if (string.IsNullOrEmpty(this.開始時間))
                {
                    return null;
                }

                return DateTime.Parse(this.開始時間.Substring(0, 10));
            }
        }

        public string 結束時間 { get; set; }
        
        [JsonIgnore]
        public DateTime? 結束日期
        {
            get
            {
                if (string.IsNullOrEmpty(this.結束時間))
                {
                    return null;
                }

                return DateTime.Parse(this.結束時間.Substring(0, 10));
            }
        }
    }
}
