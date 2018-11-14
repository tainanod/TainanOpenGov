using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// 選單
    /// </summary>
    public class MenuVm
    {
        /// <summary>
        /// 選單編號
        /// </summary>
        public Guid MenuId { get; set; }
        /// <summary>
        /// 選單名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 選單控制器
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 選單動作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 選單區域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 選單排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否開啟功能(給局處管理員用的)
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
