using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 檢視結果-統計圖表
    /// </summary>
    public class QuestGatherVm
    {
        /// <summary>
        /// info
        /// </summary>
        public Guid InfoId { get; set; }

        /// <summary>
        /// question
        /// </summary>
        public Guid QstId { get; set; }

        /// <summary>
        /// qusetTitle
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 是否開放統計結果
        /// </summary>
        public bool IsGather { get; set; } = false;

        /// <summary>
        /// 總作答數
        /// </summary>
        public int Counts { get; set; }

        /// <summary>
        /// 總作答數-題目
        /// </summary>
        public int SetCounts { get; set; }

        /// <summary>
        /// xAxis-categories
        /// </summary>
        public IEnumerable<string> Categories { get; set; } = null;

        /// <summary>
        /// series-data
        /// </summary>
        public IEnumerable<QuestGatherChartsSeriesVm> Series { get; set; } = null;
        
    }    

    /// <summary>
    /// series-data
    /// </summary>
    public class QuestGatherChartsSeriesVm
    {
        /// <summary>
        /// name
        /// </summary>
        public string PageName { get; set; } = string.Empty;

        /// <summary>
        /// counts
        /// </summary>
        public int y { get; set; }
    }
}
