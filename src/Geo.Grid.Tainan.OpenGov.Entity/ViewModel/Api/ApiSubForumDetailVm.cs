using System;
using System.Collections.Generic;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    public class ApiSubForumDetailVm
    {
        /// <summary>
        /// 論壇編號
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 論壇名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 子議題編號
        /// </summary>
        public Guid SubId { get; set; }
        /// <summary>
        /// 子議題名稱
        /// </summary>
        public string SubName { get; set; }
        /// <summary>
        /// 主題類別
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 主辦單位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 議題描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 發佈時間
        /// </summary>
        public DateTime OpenDate { get; set; }
        /// <summary>
        /// 截止時間
        /// </summary>
        public DateTime CloseDate { get; set; }
        /// <summary>
        /// 附件連結
        /// </summary>
        public IEnumerable<PageNameVm> Documents { get; set; }
    }
}