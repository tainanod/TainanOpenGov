using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 送出填寫結果Model
    /// </summary>
    public class PostQuestFillVm
    {
        /// <summary>
        /// 選項ID
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// 文字描述答案
        /// </summary>
        public string FillDesc { get; set; }
    }
}
