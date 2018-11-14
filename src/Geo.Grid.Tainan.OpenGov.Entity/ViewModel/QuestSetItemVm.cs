using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 選項&填寫VM
    /// </summary>
    public class QuestSetItemVm
    {
        /// <summary>
        /// 選項Guid
        /// </summary> 
        public Guid SetId { get; set; }

        /// <summary>
        /// 項目說明
        /// </summary>
        public string SetDesc { get; set; }

        /// <summary>
        /// 分數
        /// </summary>
        public int SetScore { get; set; }

        /// <summary>
        /// 是否要文字說明 1:是 0:否
        /// </summary>
        public bool SetNote { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SetSorting { get; set; }

        /// <summary>
        /// 選擇的答案-個人
        /// </summary>
        public QuestSetItemChooseVm SetChoose { get; set; }

        /// <summary>
        /// 選擇的答案-全部
        /// </summary>
        public IEnumerable<QuestSetItemChooseVm> SetChooseList { get; set; }

        /// <summary>
        /// 必填
        /// </summary>
        public MdQuestNecessaryRel MdQuestNecessaryRel { get; set; }
    }
}
