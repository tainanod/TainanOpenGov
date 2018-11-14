using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Engineering
{
    public class EngineeringLogVm
    {
        ///<summary>
        /// 工程標案匯入紀錄編號
        ///</summary>
        public long LogId { get; set; }

        ///<summary>
        /// 原始資料,CSV檔案格式,不換行(;分隔)
        ///</summary>
        public string LogMessage { get; set; }

        ///<summary>
        /// 檔案名稱
        ///</summary>
        public string FileName { get; set; }

        ///<summary>
        /// 建立人員
        ///</summary>
        public string CreatedBy { get; set; }

        ///<summary>
        /// 建立日期
        ///</summary>
        public DateTime CreatedDate { get; set; }

        ///<summary>
        /// 建立日期
        ///</summary>
        public string CreatedDateStr {
            get {
                return CreatedDate.ToString("yyyy-MM-dd");
            }
        }

        ///<summary>
        /// 是否啟用
        ///</summary>
        public bool IsEnabled { get; set; }
    }
}
