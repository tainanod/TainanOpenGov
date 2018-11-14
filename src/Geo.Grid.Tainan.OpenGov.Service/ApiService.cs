using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Geo.Grid.Common.Helpers;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Newtonsoft.Json;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class ApiService : IApiService
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public ApiService()
        {
        }
        

        public T GetDataFromWebApi<T>(string url, Dictionary<string, string> replaceStr = null)
        {
            string jsonString = HttpHelper.DownloadString(url);
            if (replaceStr != null && replaceStr.Keys.Count > 0)
            {
                replaceStr.Keys.ToList().ForEach(k => jsonString = jsonString.Replace(k, replaceStr[k]));
            }
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
