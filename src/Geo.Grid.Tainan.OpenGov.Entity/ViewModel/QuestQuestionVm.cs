using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class QuestQuestionVm
    {
        /// <summary>
        /// contructor
        /// </summary>
        public QuestQuestionVm()
        {
            QuestSetItemVms = new List<QuestSetItemVm>();
        }

        /// <summary>
        /// 題目Guid
        /// </summary>
        public Guid QstId { get; set; }
        /// <summary>
        /// 排序(題號)
        /// </summary>
        public int QstSorting { get; set; }
        /// <summary>
        /// 答案類型 1:單選(含是非) 2:複選 3:問答 4:多重問答
        /// </summary>
        public int QstAnsType { get; set; }
        /// <summary>
        /// 題目
        /// </summary>
        public string QstQuestion { get; set; }
        /// <summary>
        /// 0非必填 1必填
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// 選項與填寫VM
        /// </summary>
        public IEnumerable<QuestSetItemVm> QuestSetItemVms { get; set; }
    }
}
