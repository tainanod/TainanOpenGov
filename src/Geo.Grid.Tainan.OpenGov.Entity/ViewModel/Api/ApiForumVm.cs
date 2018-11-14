using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    /// <summary>
    /// 台南開放政府Restful Api
    /// 取得公民論壇之列表
    /// </summary>
    public class ApiForumVm
    {
        /// <summary>
        /// 論壇編號
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 主題
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 主辦單位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 發佈時間
        /// </summary>
        public DateTime OpenDate { get; set; }
    }
}
