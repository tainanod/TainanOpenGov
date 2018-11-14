using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Geo.Grid.Tainan.OpenGov.Service.Common
{
    public class ContentTypeExt
    {
        /// <summary>
        /// 符合限定類型的延伸檔名
        /// </summary>
        private static readonly string[] allowFileTypes = new string[] {
            "application/pdf"//,
            //"application/msword",
            //"application/mspowerpoint",
            //"application/vnd.ms-excel",
            //"application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            //"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            //"application/vnd.openxmlformats-officedocument.presentationml.presentation",
            //"application/x-rar-compressed", //winrar
            //"application/zip" //zip
        };

        private static Dictionary<string, string> allowDocumentExtension = new Dictionary<string, string>
        {
            {".pdf", "application/pdf"},
            {".doc", "application/msword"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".ppt", "application/vnd.ms-powerpoint"},
            {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"}
        };

        /// <summary>
        /// 符合圖片限定類型的延伸檔名 Hank@20140730
        /// </summary>
        private static readonly string[] allowPicFileTypes = new string[] {
            "image/jpeg"//,
            //"image/bmp",
            //"image/tiff",
        };

        /// <summary>
        /// 檢查延伸檔名是否符合限定類型
        /// </summary>
        /// <param name="contentType">檔案類型</param>
        /// <returns></returns>
        public static bool CheckFileFormat(string contentType)
        {
            return allowFileTypes.Contains(contentType.ToLower());
        }

        /// <summary>
        /// 檢查延伸檔名是否符合圖片限定類型 Hank@20140730
        /// </summary>
        /// <param name="contentType">延伸檔名的ContentType</param>
        /// <returns></returns>
        public static bool CheckPicFileFormat(string contentType)
        {
            return allowPicFileTypes.Contains(contentType.ToLower());
        }

        /// <summary>
        /// 從系統登錄檔取得檔案對應的MIME Content Type
        /// 這個方法不好，因為正式Server上不會安裝這些軟體
        /// </summary>
        /// <param name="fileName">檔名</param>
        /// <returns></returns>
        public static string GetContentTypeFromRegistry(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            if (ext.ToLower() == ".pdf") return "application/pdf";
            using (Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext))
            {
                if (registryKey == null)
                    return null;
                var value = registryKey.GetValue("Content Type");
                return (value == null) ? string.Empty : value.ToString();
            }
        }
        

        public static string GetContentTypeFromDictionary(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (!string.IsNullOrEmpty(extension))
            {
                var ext = extension.ToLower();
                if (!allowDocumentExtension.ContainsKey(ext))
                {
                    throw new Exception("上傳的檔案為不允許的檔案格式！");
                }
                else
                {
                    return allowDocumentExtension[ext];
                }
            }
            else
            {
                throw new Exception("檔案名稱並未包含附檔名！");
            }

            
            
        }

        //GetContentTypeFromRegistry
    }
}
