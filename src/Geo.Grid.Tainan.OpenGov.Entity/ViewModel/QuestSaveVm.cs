using System;
using System.Collections.Generic;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 問卷填寫-前台用
    /// </summary>
    public class QuestSaveVm
    {
        /// <summary>
        /// 問卷編號
        /// </summary>
        public Guid InfoId { get; set; }

        /// <summary>
        /// 填寫人
        /// </summary>
        public string UserMail { get; set; } = string.Empty;

        /// <summary>
        /// 選項
        /// </summary>
        public IEnumerable<PostQuestFillVm> SetItemArray { get; set; }
    }
}
