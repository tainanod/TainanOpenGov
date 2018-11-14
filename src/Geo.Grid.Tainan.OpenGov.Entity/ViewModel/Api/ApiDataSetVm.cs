using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    /// <summary>
    /// 台南開放政府Restful Api
    /// 取得資料目錄之清單
    /// </summary>
    public class ApiDataSetVm
    {
        /// <summary>
        /// 資料目錄編號
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 資料名稱
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 業管單位
        /// </summary>
        public string UnitName { get; set; }
    }
}
