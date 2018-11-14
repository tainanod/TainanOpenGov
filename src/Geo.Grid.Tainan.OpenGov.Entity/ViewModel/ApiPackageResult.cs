using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class ApiPackageResult
    {
        [JsonProperty(PropertyName = "resource_id")]
        public Guid ResourceId { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "records")]
        public List<AllowanceJsonVm> Records { get; set; }
    }
}
