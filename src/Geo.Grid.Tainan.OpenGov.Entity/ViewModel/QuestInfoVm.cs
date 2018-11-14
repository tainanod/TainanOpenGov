using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 問卷資訊Vm
    /// </summary>
    public class QuestInfoVm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public QuestInfoVm()
        {
            QuestGroupVms = new List<QuestGroupVm>();
        }

        /// <summary>
        /// 問卷資訊ID
        /// </summary>
        public Guid InfoId { get; set; }

        /// <summary>
        /// 問卷標題
        /// </summary>
        public string InfoTitle { get; set; }

        /// <summary>
        /// 問卷說明
        /// </summary>
        public string InfoDesc { get; set; }

        /// <summary>
        /// 結尾說明
        /// </summary>
        public string InfoFooter { get; set; }

        /// <summary>
        /// 開放填寫日期(起)
        /// </summary>
        public DateTime? InfoDateSt { get; set; } = DateTime.Today;

        /// <summary>
        /// 開放填寫日期(迄)
        /// </summary>
        public DateTime? InfoDateEnd { get; set; } = DateTime.Today.AddMonths(3);

        /// <summary>
        /// 群組
        /// </summary>
        public IEnumerable<QuestGroupVm> QuestGroupVms { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        public DateTime EditDate { get; set; }

        /// <summary>
        /// 驗證方式
        /// </summary>
        public int VerifyType { get; set; }

        /// <summary>
        /// 所屬公民論壇ID
        /// </summary>
        public Guid ForumId { get; set; }

        /// <summary>
        /// 問卷結果是否開放(統計表)
        /// </summary>
        public bool IsGather { get; set; }

        /// <summary>
        /// 是否開放 1:使用  0:停用
        /// </summary>
        public bool InfoValid { get; set; }

        /// <summary>
        /// 是否已填過
        /// </summary>
        public bool IsComplete { get; set; } = false;

    }
}
