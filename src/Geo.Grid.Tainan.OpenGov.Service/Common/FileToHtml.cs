using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Common;

namespace Geo.Grid.Tainan.OpenGov.Service.Common
{
    /// <summary>
    /// 取得檔案內容
    /// </summary>
    public class FileToHtml
    {
        /// <summary>
        /// 取得檔案
        /// </summary>
        /// <param name="sourcePath">檔案路徑</param>
        /// <returns></returns>
        public static Result<string> GetFileHtml(string sourcePath)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                result.Data = string.Empty;
                using (FileStream fs = File.Open(HttpContext.Current.Server.MapPath(sourcePath), FileMode.Open))
                {
                    StreamReader sr = new StreamReader(fs);
                    result.Data = sr.ReadToEnd();
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }
    }
}
