using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using NLog;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class RssService : IRssService
    {
        /// <summary>
        /// nlog
        /// </summary>
        private Logger logger = LogManager.GetCurrentClassLogger();

        public List<RssEntity> GetTop5Rss()
        {
            try
            {
                var url = "http://disaster.tainan.gov.tw/disaster/rss.asp?nsub=A00000";
                using (var wc = new WebClient() { Encoding = Encoding.UTF8 })
                {
                    var data = wc.DownloadString(url);
                    using (var reader = XmlReader.Create(new StringReader(data)))
                    {
                        var xe = XElement.Load(reader);
                        return xe.Descendants("item").Select(item => new RssEntity()
                        {
                            Title = item.Descendants("title").First().Value,
                            Link = item.Descendants("link").First().Value,
                            Guid = item.Descendants("guid").First().Value,
                            PubDate = DateTime.Parse(item.Descendants("pubDate").First().Value)
                        }).OrderByDescending(x => x.PubDate).Take(5).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return new List<RssEntity>();
            }
        }
    }
}