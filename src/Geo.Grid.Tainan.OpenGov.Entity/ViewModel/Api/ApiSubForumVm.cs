using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    public class ApiSubForumVm
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
    }
}