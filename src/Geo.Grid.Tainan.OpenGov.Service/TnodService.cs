using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.JsonViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class TnodService : ITnodService
    {
        public List<TnodDataset> GetTop5Dataset()
        {
            var url = "http://data.tainan.gov.tw/";
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                wc.Encoding = Encoding.UTF8;
                var response = wc.DownloadString(url + "datasetlist?limit=5");
                var result = JsonConvert.DeserializeObject<List<TnodDataset>>(response);
                foreach (var item in result)
                {
                    var deatil = wc.DownloadString(url + "api/3/action/package_show?id=" + item.Id);
                    dynamic d = JObject.Parse(deatil);
                    if ((bool)d.success)
                    {
                        var resources = d.result.resources;
                        if (resources.Count > 0)
                        {
                            item.FileType = resources.First.format;
                        }
                    }
                }
                return result;
            }
        }

        public List<TnodMayor> GetTop5Mayor()
        {
            var url = "http://data.tainan.gov.tw/api/action/datastore_search?resource_id=91ba78f9-02e1-4fd8-8981-e1d913f06b01&limit=5&sort=%E9%96%8B%E5%A7%8B%E6%99%82%E9%96%93%20desc";
            var response = DownloadString(url);
            //joe@20150424 轉不出來 NULL
            response = response.Replace(@"\ufeff\u6a19\u984c", "標題");
            dynamic d = JObject.Parse(response);
            if ((bool)d.success)
            {
                var records = JsonConvert.DeserializeObject<List<TnodMayor>>(d.result.records.ToString());
                return records;
            }
            return new List<TnodMayor>();
        }

        /// <summary>
        /// 公開行程
        /// </summary>
        /// <returns></returns>
        public List<PublicScheduleVm> GetPublicSchedule()
        {
            try
            {
                string startDate = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
                var url = "http://www.tainan.gov.tw/tainan/api/?key={65DF585B-3DC5-4607-9B9E-84D504EB9D7A}&start=" + startDate + "&limit=1000";
                var jsonString = DownloadString(url);
                var records = JsonConvert.DeserializeObject<List<PublicSchedule>>(jsonString);

                var datas = records.Select(x => new PublicScheduleVm
                {
                    行程事由 = x.行程事由,
                    標題 = x.標題,
                    主辦單位 = x.主辦單位,
                    坐標X = x.坐標X,
                    坐標Y = x.坐標Y,
                    行程地點 = x.行程地點,
                    開始時間 = x.開始時間,
                    開始日期 = x.開始日期,
                    結束時間 = x.結束時間,
                    結束日期 = x.結束日期,
                }).OrderByDescending(x=>x.開始時間).Take(5).ToList();

                datas.ForEach((x =>
                {
                    x._id = datas.IndexOf(x) + 1;
                }));

                return datas;
            }
            catch (Exception e)
            {
                var error = e.Message;
            }

            return new List<PublicScheduleVm>();
        }


        private string DownloadString(string url)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                wc.Encoding = Encoding.UTF8;
                return wc.DownloadString(url);
            }
        }
    }
}