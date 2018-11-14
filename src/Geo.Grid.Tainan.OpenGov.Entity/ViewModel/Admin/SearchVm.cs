using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// 查詢
    /// </summary>
    public class SearchVm
    {
        /// <summary>
        /// 關鍵字
        /// </summary>
        public string KeyWord { get; set; } = string.Empty;

        /// <summary>
        /// key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 分頁
        /// </summary>
        public int Page { get; set; } = 1;
    }
}
