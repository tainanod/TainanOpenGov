using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 選擇的項目
    /// </summary>
    public class QuestSetItemChooseVm
    {
        /// <summary>
        /// setId
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// userId
        /// </summary>
        public Guid MdFillUserId { get; set; }

        /// <summary>
        /// 回答
        /// </summary>
        public string FillDesc { get; set; } = string.Empty;
    }
}
