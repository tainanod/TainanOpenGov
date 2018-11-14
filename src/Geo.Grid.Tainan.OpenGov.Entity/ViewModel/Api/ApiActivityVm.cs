using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    /// <summary>
    /// 活動/工作坊
    /// </summary>
    public class ApiActivityVm
    {
        /// <summary>
        /// 論壇編號
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 論壇主題
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 活動名稱/工作坊名稱
        /// </summary>
        public string ActName { get; set; }
        /// <summary>
        /// 日期與時間
        /// </summary>
        public string HoldDate { get; set; }
        /// <summary>
        /// 地點
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 說明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 附件連結
        /// </summary>
        public IEnumerable<PageNameVm> Documents { get; set; }
    }
}
