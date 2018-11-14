﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 投票管理-統計圖表
    /// </summary>
    public class VoteGatherVm
    {
        /// <summary>
        /// 主key
        /// </summary>
        public string PageId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 是否開放結束
        /// </summary>
        public bool CanVote { get; set; } = false;

        /// <summary>
        /// counts
        /// </summary>
        public int Counts { get; set; }

        /// <summary>
        /// xAxis-categories
        /// </summary>
        public IEnumerable<string> Categories { get; set; } = null;

        /// <summary>
        /// series-data
        /// </summary>
        public IEnumerable<VoteGatherChartsSeriesVm> Series { get; set; } = null;

    }

    /// <summary>
    /// series-data
    /// </summary>
    public class VoteGatherChartsSeriesVm
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
