using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 統計圖表-問卷資訊
    /// </summary>
    public class QuestGatherInfoVm
    {
        /// <summary>
        /// 論壇id
        /// </summary>
        public Guid ForumId { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        public Guid InfoId { get; set; }

        /// <summary>
        /// title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 數量
        /// </summary>
        public int Counts { get; set; }
    }
}
