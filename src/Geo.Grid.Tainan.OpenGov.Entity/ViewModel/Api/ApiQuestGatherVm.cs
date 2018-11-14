using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    public class ApiQuestGatherVm
    {
        /// <summary>
        /// 論壇編碼
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 論壇名稱
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
        /// <summary>
        /// 題目名稱
        /// </summary>
        public string QstName { get; set; }
        /// <summary>
        /// 統計資訊
        /// </summary>
        public int QstCount { get; set; }
    }
}
