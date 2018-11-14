using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class QuestNecessaryRelVm
    {
        /// <summary>
        /// 選項GUID
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// 目標Group ID
        /// </summary>
        public Guid? GroupId { get; set; }

        /// <summary>
        /// 目標Quest ID
        /// </summary>
        public Guid? QuestionId { get; set; }
    }
}
