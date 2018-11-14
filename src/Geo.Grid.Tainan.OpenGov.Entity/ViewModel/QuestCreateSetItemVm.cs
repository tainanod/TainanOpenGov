using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 問卷管理-選項-填寫用
    /// </summary>
    public class QuestCreateSetItemVm
    {
        /// <summary>
        /// set
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// qst
        /// </summary>
        public Guid QstId { get; set; }

        /// <summary>
        /// group
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// info
        /// </summary>
        public Guid InfoId { get; set; }
    }
}
