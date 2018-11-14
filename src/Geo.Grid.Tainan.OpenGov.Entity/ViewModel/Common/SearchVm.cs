using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Common
{
    /// <summary>
    /// search
    /// </summary>
    public class SearchVm
    {
        /// <summary>
        /// 目前頁面
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// 每頁幾筆
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
