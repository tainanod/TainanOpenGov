using System;
using System.Collections.Generic;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 問卷結果-匯出
    /// </summary>
    public class QuestGatherExcelVm
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid QstId { get; set; }

        /// <summary>
        /// 題目標題
        /// </summary>
        public string QstQuestion { get; set; }
    }    
}
