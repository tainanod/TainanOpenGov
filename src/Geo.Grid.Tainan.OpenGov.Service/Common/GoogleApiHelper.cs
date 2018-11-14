using System.Net;
using Newtonsoft.Json;

namespace Geo.Grid.Tainan.OpenGov.Service.Common
{
    /// <summary>
    /// Google Api Helper
    /// </summary>
    public class GoogleApiHelper
    {
        /// <summary>
        /// 實作GoogleApi取得圖片時的Class
        /// </summary>
        public class GoogleImage
        {
            public Image Image { get; set; }
        }

        /// <summary>
        /// 實作GoogleApi取得圖片時的Class Detail
        /// </summary>
        public class Image
        {
            public string Url { get; set; }

            public bool IsDefault { get; set; }
        }

        /// <summary>
        /// 透過GoogleApi取得Google User ImageUrl
        /// </summary>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public static string GetGoogleUserImageUrl(string providerKey)
        {
            var imageUrl = string.Empty;
            var googleApiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GoogleAPIKey"];
            using (var wcg = new WebClient())
            {
                var url = string.Format("https://www.googleapis.com/plus/v1/people/{0}?fields=image&key={1}"
                                        , providerKey, googleApiKey);
                try
                {
                    var result = JsonConvert.DeserializeObject<GoogleImage>(wcg.DownloadString(url));
                    if (!string.IsNullOrEmpty(result.Image.Url))
                    {
                        imageUrl = result.Image.Url;
                    }
                }
                catch (System.Exception) { }

            }
            return imageUrl;
        }
    }
}