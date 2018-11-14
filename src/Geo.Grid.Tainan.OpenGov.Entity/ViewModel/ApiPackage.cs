using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class ApiPackage
    {
        /// <summary>
        /// 說明連結位置
        /// </summary>
        [JsonProperty(PropertyName = "help")]
        public string Help { get; set; }

        /// <summary>
        /// API 查詢回傳狀態
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// Package Search 回傳結果資訊
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public ApiPackageResult Result { get; set; }
    }
}
