using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 問題群組VM
    /// </summary>
    public class QuestGroupVm
    {
        public QuestGroupVm()
        {
            MdQuestions = new List<QuestQuestionVm>();
        }

        /// <summary>
        /// 所屬問卷ID
        /// </summary>
        public Guid InfoId { get; set; }
        /// <summary>
        /// 群組Guid
        /// </summary>
        public Guid GroupId { get; set; }
        /// <summary>
        /// 群組標題
        /// </summary>
        public string GroupTitle { get; set; }
        /// <summary>
        /// 群組說明
        /// </summary>
        public string GroupDesc { get; set; }
        /// <summary>
        /// 群組順序
        /// </summary>
        public int GroupSort { get; set; }
        /// <summary>
        /// 群組底下的問題 
        /// </summary>
        public virtual IEnumerable<QuestQuestionVm> MdQuestions { get; set; }
    }
}
