using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    /// <summary>
    /// 台南開放政府Restful Api
    /// 問卷清單
    /// </summary>
    public class ApiQuestInfoVm
    {
        /// <summary>
        /// 論壇編碼
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 論壇主題
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 問卷編碼
        /// </summary>
        public Guid QiId { get; set; }
        /// <summary>
        /// 問卷名稱
        /// </summary>
        public string QiName { get; set; }
    }
}
