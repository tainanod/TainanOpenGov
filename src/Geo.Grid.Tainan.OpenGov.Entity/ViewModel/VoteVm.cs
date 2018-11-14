using System;
using System.Collections.Generic;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 投票管理
    /// </summary>
    public class VoteVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid VoteId { get; set; }

        /// <summary>
        /// 公民論壇編號
        /// </summary>
        public Guid ForumId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 說明
        /// </summary>
        public string Info { get; set; } = string.Empty;

        /// <summary>
        /// 投票開始日
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 是否開放投票結果
        /// </summary>
        public bool CanVote { get; set; }

        /// <summary>
        /// 投票結束日
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 最多須投幾票
        /// </summary>
        public int SelectNumber { get; set; }

        /// <summary>
        /// 是否已投過票
        /// </summary>
        public bool IsVote { get; set; } = false;

        /// <summary>
        /// 驗證方式
        /// </summary>
        public int VerifyType { get; set; }

        /// <summary>
        /// 選項
        /// </summary>
        public IEnumerable<GuidNameVm> OptionArray { get; set; }

        /// <summary>
        /// 個資
        /// </summary>
        public IEnumerable<VoteBasicGroupVm> BasicArray { get; set; }
    }
}
