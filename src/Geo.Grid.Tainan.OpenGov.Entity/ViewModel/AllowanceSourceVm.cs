using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class AllowanceSourceVm
    {
        ///<summary>
        /// 公益臺南-資料集來源管理
        ///</summary>
        public Guid SourceId { get; set; }

        ///<summary>
        /// 資料集名稱
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// 管理組織
        ///</summary>
        public string Organization { get; set; }

        ///<summary>
        /// 網頁連結
        ///</summary>
        public string WebSite { get; set; }

        ///<summary>
        /// API
        ///</summary>
        public string ApiUrl { get; set; }

        ///<summary>
        /// 資料來源的唯一編碼，由ApiUrl取得
        ///</summary>
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 補助 資料
        /// </summary>
        public List<AllowanceVm> Allowances { get; set; }
    }
}
